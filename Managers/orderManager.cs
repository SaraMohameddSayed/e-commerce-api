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

    public DbContext dbContext;
    public orderManager(dbContext _context, productManager _productManager, UserManager<IdentityUser> _userManager, cartProductManager _cartProductManager, governorateManager _governorateManager, orderProductManager _orderProductManager) : base(_context)
    {
        productManager = _productManager;
        userManager = _userManager;
        cartProductManager = _cartProductManager;
        governorateManager = _governorateManager;
        dbContext = _context;
        orderProductManager = _orderProductManager;
    }

    public async Task<Order> CreateOrderFromCart(addOrderViewModel addorderViewModel)
    {
        var cartProducts = await cartProductManager.getAll().Where(cp => cp.cart.userId == addorderViewModel.userId).ToListAsync();
        var subTotal = await productManager.CalculateProductsTotal(cartProducts);
        var orderProducts = orderProductManager.GenerateOrderProductsFromCartProducts(cartProducts);
        var governorate = await governorateManager.getAll()
     .FirstOrDefaultAsync(g => g.id == addorderViewModel.governorateId);

        if (governorate == null)
            throw new Exception("Governorate not supported");
        var order = new Order
        {
            userId = addorderViewModel.userId,
            user = userManager.Users.FirstOrDefault(u => u.Id == addorderViewModel.userId),
            governorateId = addorderViewModel.governorateId,
            areaId = addorderViewModel.areaId,
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
}