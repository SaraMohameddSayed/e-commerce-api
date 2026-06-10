using CloudinaryDotNet.Actions;
using Infrastructure;
using Services.Interfaces;
using Application.Events;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Domain;
using System;
using System.Threading.Tasks;
using DTOs;
using Microsoft.AspNetCore.Identity;
using Application.Shared.Common;
namespace Application.Services;;

public class ProductService : MainService<Product>
{
    private readonly AppDbContext  _AppDbContext ;
    private readonly IEventBus _eventBus;
    private readonly UserManager<IdentityUser> _userManager;
    public ProductService(AppDbContext  context,IEventBus eventBus,UserManager<IdentityUser> userManager) : base(context)
    {
        _AppDbContext =context;
        _eventBus = eventBus;
        _userManager = userManager;
    }
    public async Task<decimal> CalculateProductsTotal(IEnumerable<CartProduct> cartProducts)
    {
        decimal totalAmount = 0;
        foreach (var cartProduct in cartProducts)
        {
            var product =await GetOne(cartProduct.ProductId);

            decimal discountValue = product.Offers?
                .OrderByDescending(o => o.ApplicationDate)
                .FirstOrDefault()?.DiscountValue ?? 0;
            decimal discountedPrice = discountValue > 0 ? product.Price - discountValue : product.Price;

            // تحديث إجمالي الطلب
            totalAmount += discountedPrice * cartProduct.Quantity;
        }
        return totalAmount;
    }

    public async Task<PagedResult<ProductResponse>> GetPagedProducts(int? categoryId,string? searchText,int pageNumber, int pageSize)
    {
        pageNumber = pageNumber < 1 ? 1 : pageNumber;
        pageSize = pageSize > 9 ? 9 : pageSize;
        IQueryable<Product> query = _AppDbContext.Set<Product>()
            .Include(p => p.Category)
            .Include(p => p.Offers)
                .ThenInclude(po => po.Offer);
                


        if (categoryId!=null)
        {

            query=query.Where(p => p.Category.Id == categoryId);

        }
        if (!string.IsNullOrWhiteSpace(searchText))
        {
           query= query.Where(p => p.Name.Contains(searchText) || p.Description.Contains(searchText));
        }
        int totalCount = query.Count();

        var pagedItems = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Select(p=>p.ToResponse())
            .ToListAsync();
        return new PagedResult<ProductResponse>
        {
            Items = pagedItems,
            TotalCount = totalCount,
            PageNumber = pageNumber,
            PageSize = pageSize
        };


    }

public async Task<decimal> CalculateDiscountedPrice(int productId)
    {
        var product = await GetOne(productId);
        decimal discountValue = product.Offers?
            .OrderByDescending(o => o.ApplicationDate)
            .FirstOrDefault()?.DiscountValue ?? 0;
        decimal discountedPrice = discountValue > 0 ? product.Price - discountValue : product.Price;
        return discountedPrice;
    }

public async void UpdateProductQuantity(int productId, int quantityToDeduct)
    {
        var product = await GetOne(productId);
        if (product != null)
        {
            product.Quantity -= quantityToDeduct;
            await _AppDbContext.SaveChangesAsync();
        }
    }

    public async Task<bool> UpdateProduct(UpdateProductRequest updateProductRequest)
    {
        var product = await GetOne(updateProductRequest.Id);
        product!.Name = updateProductRequest.Name;
        product.Description = updateProductRequest.Description;
        product.Price = updateProductRequest.Price;
        product.Quantity = updateProductRequest.Quantity;
        product.CategoryId = updateProductRequest.CategoryId;
        product.ImageUrl = updateProductRequest.ImageUrl!;
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

    public async Task<bool> AddProductAsync(AddProductRequest addProductRequest)
    {
      var result=  await Add(addProductRequest.ToProduct());
        if (result)
        {
            var product = await _AppDbContext.Set<Product>().FirstOrDefaultAsync(p => p.Name == addProductRequest.Name && p.Price == addProductRequest.Price);
            
            var adminUsers = await _userManager.GetUsersInRoleAsync("Admin");
            var adminIds = adminUsers.Select(u => u.Id).ToHashSet();
            var userIds = await _AppDbContext.Set<IdentityUser>()
          .Where(u => !adminIds.Contains(u.Id))
          .Select(u => u.Id)
          .ToListAsync();

            await _eventBus.Publish(new NewProductAddedEvent(userIds,product.Id));
            
        }
        return result;
    }
    public async Task<bool> SoftDeleteProduct(int productId)
    {
        var product = await GetOne(productId);
        if (product != null)
        {
            product.IsActive = false;
            await _AppDbContext.SaveChangesAsync();
            return true;
        }
        return false;
    }
}
