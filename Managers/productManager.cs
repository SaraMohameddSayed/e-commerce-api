using CloudinaryDotNet.Actions;
using Infrastructure;
using Microsoft.Identity.Client;
using Models;
using System;
namespace Managers;

public class productManager : MainManager<Product>
{

    public productManager(dbContext _context) : base(_context)
    {

    }
    public async Task<decimal> CalculateProductsTotal(IEnumerable<CartProduct> cartProducts,Order order)
    {
        decimal totalAmount = 0;
        foreach (var cartProduct in cartProducts)
        {
            var product =await getOne(cartProduct.productId);

            decimal discountValue = product.offers?
                .OrderByDescending(o => o.applicationDate)
                .FirstOrDefault()?.discountValue ?? 0;

            decimal discountedPrice = discountValue > 0 ? product.price - discountValue : product.price;

            order.products.Add(new OrderProduct
            {
                productId = cartProduct.productId,
                quantity = cartProduct.quantity,
                price = discountedPrice
            });

            // خصم الكمية من المخزون
            if (product != null)
            {
                product.quantity -= cartProduct.quantity;
            }
            // تحديث إجمالي الطلب
            totalAmount += discountedPrice * cartProduct.quantity;
        }
        return totalAmount;
    }
}