using Application.Shared.Common;
using Infrastructure;
using Application.Events;
using Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Domain;
using Domain.Enums;
using DTOs;
namespace Application.Services;;

public class OrderService : MainService<Order>
{
    private readonly ProductService _productService;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly CartProductService _cartProductService;
    private readonly OrderProductService _orderProductService;
    private readonly GovernorateService _governorateService;
    private readonly AreaService _areaService;
    private readonly IEventBus _eventBus;
    private readonly AppDbContext  _AppDbContext ;
    public OrderService(AppDbContext AppDbContext, ProductService productService, UserManager<IdentityUser> userManager,
        CartProductService cartProductService, GovernorateService governorateService, 
        OrderProductService orderProductService, 
        AreaService areaService,IEventBus eventBus) : base(AppDbContext)
    {
        _productService = productService;
        _userManager = userManager;
        _cartProductService = cartProductService;
        _governorateService = governorateService;
        _AppDbContext  = AppDbContext;
        _orderProductService = orderProductService;
        _areaService = areaService;
        _eventBus = eventBus;
    }

    public async Task<Order> CreateOrderFromCart(AddOrderRequest addorderRequest)
    {
        var cartProducts = await _cartProductService.GetAll().Where(cp => cp.Cart.UserId == addorderRequest.UserId).ToListAsync();
        var subTotal = await _productService.CalculateProductsTotal(cartProducts);
        var orderProducts = _orderProductService.GenerateOrderProductsFromCartProducts(cartProducts);
        var governorate = await _governorateService.GetAll()
     .FirstOrDefaultAsync(g => g.Id == addorderRequest.GovernorateId);
        var area = await _areaService.GetAll()
     .FirstOrDefaultAsync(a => a.Id == addorderRequest.AreaId);

        if (governorate == null)
            throw new Exception("Governorate not supported");
        var order = new Order
        {
            UserId = addorderRequest.UserId,
            User = _userManager.Users.FirstOrDefault(u => u.Id == addorderRequest.UserId),
            GovernorateName=governorate.Name,
            AreaName=area.Name,
            Address = addorderRequest.Address,
            Phone = addorderRequest.Phone,
            PaymentMethod = addorderRequest.PaymentMethod,
            DeliveryFee = area.DeliveryFee,
            Notes = addorderRequest.Notes,
            Status = OrderStatus.Pending,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now,
            TrackingNumber = $"ORD-{DateTime.UtcNow:yyMMddHHmmss}",
            SubTotal = subTotal,
            TotalAmount = subTotal + area.DeliveryFee,
            Products = orderProducts
        };

        try
        {
        await _AppDbContext.Set<Order>().AddAsync(order);
        await _AppDbContext.SaveChangesAsync();
            var admins = await _userManager.GetUsersInRoleAsync("Admin");
            // حذف المنتجات من السلة بعد إنشاء الطلب
            await _cartProductService.ClearCartByUserId(addorderRequest.UserId);
            foreach (var admin in admins)
            {
                await _eventBus.Publish(new NewOrderAddedEvent(admin.Id, order.Id));
            }
            return order;
        }
        catch
        {
            throw;
        }
    }

    public async Task<PagedResult<OrderResponse>> GetPagedOrders(string? trackingNumber,OrderStatus? orderStatus ,int pageNumber, int pageSize)
    {
        pageNumber = pageNumber < 1 ? 1 : pageNumber;
        pageSize = pageSize > 9 ? 9 : pageSize;
        IQueryable<Order> query = _AppDbContext.Set<Order>()
            .Include(o => o.User)
             .Include(o => o.Products)
                .ThenInclude(o => o.Product)
            .OrderByDescending(o => o.CreatedAt);
        if (trackingNumber != null)
        {
           query=query.Where(o => o.TrackingNumber == trackingNumber);
        }
        if(orderStatus != null)
        {
           query= query.Where(o => o.Status == orderStatus);
        }
        int totalCount = await query.CountAsync();
        var pagedItems = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Select(o => o.ToResponse())
            .ToListAsync();
        return new PagedResult<OrderResponse>
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
        order.Status = newStatus;
        order.UpdatedAt = DateTime.Now;
        var updated = await Update(order);
        if (!updated)
            return false;
      await  _eventBus.Publish(new OrderStatusChangedEvent(order.UserId, order.Id, newStatus.ToString()));
        return true;


    }
}
