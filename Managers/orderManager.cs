using Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.Enums;
using System;
using ViewModels;
using Microsoft.EntityFrameworkCore;
namespace Managers;

public class orderManager : MainManager<Order>
{
    public productManager productManager;
    public UserManager<IdentityUser> userManager;
    public cartProductManager cartProductManager;
    public orderProductManager orderProductManager;
    public governorateManager governorateManager;
    public areaManager areaManager;
    public DbContext dbContext;
    public orderManager(dbContext _context, productManager _productManager, UserManager<IdentityUser> _userManager, cartProductManager _cartProductManager, governorateManager _governorateManager, orderProductManager _orderProductManager, areaManager _areaManager) : base(_context)
    {
        productManager = _productManager;
        userManager = _userManager;
        cartProductManager = _cartProductManager;
        governorateManager = _governorateManager;
        dbContext = _context;
        orderProductManager = _orderProductManager;
        this.areaManager = _areaManager;
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
            governorateId = addorderViewModel.governorateId,
            governorateName=governorate.name,
            areaId = addorderViewModel.areaId,
            areaName=area.name,
            address = addorderViewModel.Address,
            phone = addorderViewModel.Phone,
            paymentMethod = addorderViewModel.PaymentMethod,
            delivaryFee = governorate.deliveryFee,
            notes = addorderViewModel.Notes,
            status = OrderStatus.Pending,
            createdAt = DateTime.Now,
            updatedAt = DateTime.Now,
            trackingNumber = $"ORD-{DateTime.UtcNow:yyMMddHHmmss}",
            subTotal=subTotal,
            totalAmount=subTotal + governorate.deliveryFee,
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
            .Include(o => o.governorate)
                .ThenInclude(g => g.areas)
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

}