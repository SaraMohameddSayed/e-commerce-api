using Application.Shared;
using Infrastructure;
using Services.Events;
using Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;
using Domain;
using Domain.Enums;
using System;
using DTOs;
namespace Services;

public class OrderService : MainService<Order>
{
    public ProductService _productService;
    public UserManager<IdentityUser> _userManager;
    public CartProductService _cartProductService;
    public OrderProductService _orderProductService;
    public GovernorateService _governorateService;
    public AreaService _areaService;
    public IEventBus _eventBus;
    public DbContext _dbContext;
    public OrderService(dbContext context, ProductService productService, UserManager<IdentityUser> userManager,
        CartProductService cartProductService, GovernorateService governorateService, 
        OrderProductService orderProductService, 
        AreaService areaService,IEventBus eventBus) : base(context)
    {
        _productService = productService;
        _userManager = userManager;
        _cartProductService = cartProductService;
        _governorateService = governorateService;
        _dbContext = context;
        _orderProductService = orderProductService;
        _areaService = areaService;
        _eventBus = eventBus;
    }

    public async Task<Order> CreateOrderFromCart(addOrderViewModel addorderViewModel)
    {
        var cartProducts = await _cartProductService.GetAll().Where(cp => cp.cart.userId == addorderViewModel.userId).ToListAsync();
        var subTotal = await _productService.CalculateProductsTotal(cartProducts);
        var orderProducts = _orderProductService.GenerateOrderProductsFromCartProducts(cartProducts);
        var governorate = await _governorateService.GetAll()
     .FirstOrDefaultAsync(g => g.id == addorderViewModel.governorateId);
        var area = await _areaService.GetAll()
     .FirstOrDefaultAsync(a => a.id == addorderViewModel.areaId);

        if (governorate == null)
            throw new Exception("Governorate not supported");
        var order = new Order
        {
            userId = addorderViewModel.userId,
            user = _userManager.Users.FirstOrDefault(u => u.Id == addorderViewModel.userId),
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
            var admins = await _userManager.GetUsersInRoleAsync("Admin");
            // حذف المنتجات من السلة بعد إنشاء الطلب
            await _cartProductService.ClearCartByUserId(addorderViewModel.userId);
            foreach (var admin in admins)
            {
                await _eventBus.Publish(new NewOrderAddedEvent(admin.Id, order.id));
            }
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

    public async Task<bool> UpdateOrderStatus(int orderId ,OrderStatus newStatus)
    {
        var order = await GetOne(orderId);
        if (order == null)
        {
            return false;
        }
        order.status = newStatus;
        order.updatedAt = DateTime.Now;
        var updated = await Update(order);
        if (!updated)
            return false;
      await  _eventBus.Publish(new OrderStatusChangedEvent(order.userId, order.id, newStatus.ToString()));
        return true;


    }
}
