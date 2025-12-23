using CloudinaryDotNet.Actions;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Models;
using System;
using System.Threading.Tasks;
using ViewModels;
namespace Managers;

public class productManager : MainManager<Product>
{
    public dbContext dbContext;
    public productManager(dbContext _context) : base(_context)
    {
        dbContext=_context;
    }
    public async Task<decimal> CalculateProductsTotal(IEnumerable<CartProduct> cartProducts)
    {
        decimal totalAmount = 0;
        foreach (var cartProduct in cartProducts)
        {
            var product =await getOne(cartProduct.productId);

            decimal discountValue = product.offers?
                .OrderByDescending(o => o.applicationDate)
                .FirstOrDefault()?.discountValue ?? 0;

            decimal discountedPrice = discountValue > 0 ? product.price - discountValue : product.price;

           
            // تحديث إجمالي الطلب
            totalAmount += discountedPrice * cartProduct.quantity;
        }
        return totalAmount;
    }

    public async Task<PagedResult<ProductViewModel>> GetPagedProducts(int? categoryId,string? searchText,int pageNumber, int pageSize)
    {
        IQueryable<Product> query = dbContext.Set<Product>()
            .Include(p => p.category)
            .Include(p => p.offers)
                .ThenInclude(po => po.offer);
            

       
        if (categoryId!=null)
        {

            query=query.Where(p => p.category.id == categoryId);

        }
        if (!string.IsNullOrWhiteSpace(searchText))
        {
           query= query.Where(p => p.name.Contains(searchText) || p.description.Contains(searchText));
        }
        int totalCount = query.Count();

        var pagedItems = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Select(p=>p.toViewModel())
            .ToListAsync();
        return new PagedResult<ProductViewModel>
        {
            Items = pagedItems,
            TotalCount = totalCount,
            PageNumber = pageNumber,
            PageSize = pageSize
        };


    }

public async Task<decimal> CalculateDiscountedPrice(int productId)
    {
        var product = await getOne(productId);
        decimal discountValue = product.offers?
            .OrderByDescending(o => o.applicationDate)
            .FirstOrDefault()?.discountValue ?? 0;
        decimal discountedPrice = discountValue > 0 ? product.price - discountValue : product.price;
        return discountedPrice;
    }

public async void UpdateProductQuantity(int productId, int quantityToDeduct)
    {
        var product = await getOne(productId);
        if (product != null)
        {
            product.quantity -= quantityToDeduct;
            await dbContext.SaveChangesAsync();
        }
    }

    public async Task<bool> updateProduct(updateProductViewModel _updateProductViewModel)
    {
        var product = await getOne(_updateProductViewModel.id);
        product!.name = _updateProductViewModel.name;
        product.description = _updateProductViewModel.description;
        product.price = _updateProductViewModel.price;
        product.quantity = _updateProductViewModel.quantity;
        product.categoryId = _updateProductViewModel.categoryId;
        product.imageUrl = _updateProductViewModel.imageUrl!;
        try
        {
            await Update(product);
            return true;
        }
        catch 
        {
            throw;
        }
    }
}
