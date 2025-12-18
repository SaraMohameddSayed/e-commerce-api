using CloudinaryDotNet.Actions;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Models;
using System;
using ViewModels;
namespace Managers;

public class productManager : MainManager<Product>
{
    public dbContext dbContext;
    public productManager(dbContext _context) : base(_context)
    {
        dbContext=_context;
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

    public (List<ProductViewModel> Items, int TotalCount) GetPagedProducts(int pageNumber, int pageSize)
    {
        var query = dbContext.Set<Product>()
            .Include(p => p.category)
            .Include(p => p.offers)
                .ThenInclude(po => po.offer)
            .Select(p => p.toViewModel());

        int totalCount = query.Count();

        var items = query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        return (items, totalCount);
    }
}
