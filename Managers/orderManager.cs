using Infrastructure;
using Managers.Events;
using Managers.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.Enums;
using System;
using ViewModels;
namespace Managers;

public class orderManager : MainManager<Order>
{
    public productManager productManager;
    public UserManager<IdentityUser> userManager;
    public cartProductManager cartProductManager;
    public orderProductManager orderProductManager;
    public governorateManager governorateManager;
    public areaManager areaManager;
    public IEventBus eventBus;
    public DbContext dbContext;
    public orderManager(dbContext _context, productManager _productManager, UserManager<IdentityUser> _userManager, cartProductManager _cartProductManager, governorateManager _governorateManager, orderProductManager _orderProductManager, areaManager _areaManager,IEventBus _eventBus) : base(_context)
    {
        productManager = _productManager;
        userManager = _userManager;
        cartProductManager = _cartProductManager;
        governorateManager = _governorateManager;
        dbContext = _context;
        orderProductManager = _orderProductManager;
        areaManager = _areaManager;
        eventBus = _eventBus;
    }

    public async Task<Order> CreateOrderFromCart(addOrderViewModel addorderViewModel)
    {
        var cartProducts = await cartProductManager.getAll().Where(cp => cp.cart.userId == addorderViewModel.userId).ToListAsync();
        var subTotal = await productManager.CalculateProductsTotal(cartProducts);
        var orderProducts = orderProductManager.GenerateOrderProductsFromCartProducts(cartProducts);
        var governorate = await governorateManager.getAll()
     .FirstOrDefaultAsync(g => g.id == addorderViewModel.governorateId);
        var area = await areaManager.getAll()
     .FirstOrDefaultAsync(a => a.id == addorderViewModel.areaId);

        if (governorate == null)
            throw new Exception("Governorate not supported");
        var order = new Order
        {
            userId = addorderViewModel.userId,
            user = userManager.Users.FirstOrDefault(u => u.Id == addorderViewModel.userId),
            governorateName=governorate.name,
            areaName=area.name,
            address = addorderViewModel.Address,
            phone = addorderViewModel.Phone,
            paymentMethod = addorderViewModel.PaymentMethod,
            delivaryFee = area.deliveryFee,
            notes = addorderViewModel.Notes,
            status = OrderStatus.Pending,
            createdAt = DateTime.Now,
            updatedAt = DateTime.Now,
            trackingNumber = $"ORD-{DateTime.UtcNow:yyMMddHHmmss}",
            subTotal=subTotal,
            totalAmount=subTotal + area.deliveryFee,
            products = orderProducts
        };

        try
        {
        await dbContext.Set<Order>().AddAsync(order);
        await dbContext.SaveChangesAsync();
            // حذف المنتجات من السلة بعد إنشاء الطلب
           await cartProductManager.clearCartByUserId(addorderViewModel.userId); 
            return order;
        }
        catch
        {
            throw;
        }
    }

    public async Task<PagedResult<orderViewModel>> GetPagedOrders(string? trackingNumber,OrderStatus? orderStatus ,int pageNumber, int pageSize)
    {
        pageNumber = pageNumber < 1 ? 1 : pageNumber;
        pageSize = pageSize > 9 ? 9 : pageSize;
        IQueryable<Order> query = dbContext.Set<Order>()
            .Include(o => o.user)
             .Include(o => o.products)
                .ThenInclude(o => o.product)
            .OrderByDescending(o => o.createdAt);
        if (trackingNumber != null)
        {
           query=query.Where(o => o.trackingNumber == trackingNumber);
        }
        if(orderStatus != null)
        {
           query= query.Where(o => o.status == orderStatus);
        }
        int totalCount = await query.CountAsync();
        var pagedItems = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Select(o => o.toViewModel())
            .ToListAsync();
        return new PagedResult<orderViewModel>
        {
            Items = pagedItems,
            TotalCount = totalCount,
            PageNumber = pageNumber,
            PageSize = pageSize
        };
    }

    public async Task<bool> updateOrderStatus(int orderId ,OrderStatus newStatus)
    {
        var order = await getOne(orderId);
        if (order == null)
        {
            return false;
        }
        order.status = newStatus;
        order.updatedAt = DateTime.Now;
        var updated = await Update(order);
        if (!updated)
            return false;
      await  eventBus.Publish(new OrderStatusChangedEvent(order.userId, order.id, newStatus.ToString()));
        return true;


    }
}
