using System;
using Domain;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public static class productOfferExtensions
    {
        public static ProductOffer toModel(this addProductToOfferViewModel vm, decimal _discountValue)
        {
            return new ProductOffer
            {
                productId = vm.productId,
                offerId = vm.offerId,
                applicationDate = vm.applicationDate,
                discountValue = _discountValue
            };
        }

        public static ProductWithOfferViewModel toViewModel(this ProductOffer model)
        {
            return new ProductWithOfferViewModel
            {
                Id = model.productId,
                Name = model.product != null ? model.product.name : string.Empty,
                imageUrl = model.product != null ? model.product.imageUrl : string.Empty,
                categoryId = model.product != null ? model.product.categoryId.ToString() : string.Empty,
                quantity = model.product != null ? model.product.quantity : 0,
                categoryName = model.product != null && model.product.category != null ? model.product.category.name : string.Empty,
                offerId = model.offerId,
                offerName = model.offer != null ? model.offer.name : string.Empty,
                discountValue = model.discountValue,
                originalPrice = model.product != null ? model.product.price : 0,
                discountedPrice = model.product != null ? model.product.price - model.discountValue : 0,
                applicationDate = model.applicationDate
            };
        }
    }
}
