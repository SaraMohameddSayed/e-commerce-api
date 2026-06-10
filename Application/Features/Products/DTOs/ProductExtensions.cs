using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
namespace DTOs
{
    public static class ProductExtensions
    {
        public static ProductResponse ToResponse(this Product product)
        {

            var discountValue = product.Offers?.OrderByDescending(p => p.ApplicationDate)?.FirstOrDefault()?.DiscountValue ?? 0;
            var discountedPrice = discountValue > 0 ?
            product.Price - discountValue : product.Price;
            var offerName = product.Offers != null && product.Offers.Count > 0 ? product.Offers.OrderByDescending(p => p.ApplicationDate).FirstOrDefault()?.Offer?.Name ?? string.Empty : string.Empty;
            return new ProductResponse
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                OriginalPrice = product.Price,
                DiscountedPrice = discountedPrice,
                ImageUrl = product.ImageUrl,
                Quantity = product.Quantity,
                CategoryId = product.CategoryId,
                CategoryName = product.Category != null ? product.Category.Name : string.Empty,
                OfferName = offerName,
                IsActive= product.IsActive
            };
        }
        public static Product ToProduct(this AddProductRequest addproductRequest)
        {
            return new Product
            {
                Name = addproductRequest.Name,
                Description = addproductRequest.Description,
                Price = addproductRequest.Price,
                Quantity = addproductRequest.Quantity,
                CategoryId = addproductRequest.CategoryId,
                ImageUrl = addproductRequest.ImageUrl ?? string.Empty
            };

        }
       
    }
}