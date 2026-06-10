using System;
using Domain;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public static class ProductOfferExtensions
    {
        public static ProductOffer ToProductOffer(this AddProductToOfferRequest vm, decimal _discountValue)
        {
            return new ProductOffer
            {
                ProductId = vm.ProductId,
                OfferId = vm.OfferId,
                ApplicationDate = vm.ApplicationDate,
                DiscountValue = _discountValue
            };
        }

        public static ProductWithOfferResponse ToResponse(this ProductOffer model)
        {
            return new ProductWithOfferResponse
            {
                Id = model.ProductId,
                Name = model.Product != null ? model.Product.Name : string.Empty,
                ImageUrl = model.Product != null ? model.Product.ImageUrl : string.Empty,
                CategoryId = model.Product != null ? model.Product.CategoryId.ToString() : string.Empty,
                Quantity = model.Product != null ? model.Product.Quantity : 0,
                CategoryName = model.Product != null && model.Product.Category != null ? model.Product.Category.Name : string.Empty,
                OfferId = model.OfferId,
                OfferName = model.Offer != null ? model.Offer.Name : string.Empty,
                DiscountValue = model.DiscountValue,
                OriginalPrice = model.Product != null ? model.Product.Price : 0,
                DiscountedPrice = model.Product != null ? model.Product.Price - model.DiscountValue : 0,
                ApplicationDate = model.ApplicationDate
            };
        }
    }
}
