using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
namespace ViewModels
{
    public static class productExtensions
    {
        public static ProductViewModel toViewModel(this Product product)
        {

            var discountValue = product.offers?.OrderByDescending(p => p.applicationDate)?.FirstOrDefault()?.discountValue ?? 0;
            var discountedPrice = discountValue > 0 ?
            product.price - discountValue : product.price;
            var offerName = product.offers != null && product.offers.Count > 0 ? product.offers.OrderByDescending(p => p.applicationDate).FirstOrDefault()?.offer?.name ?? string.Empty : string.Empty;
            return new ProductViewModel
            {
                id = product.id,
                name = product.name,
                description = product.description,
                originalPrice = product.price,
                discountedPrice = discountedPrice,
                imageUrl = product.imageUrl,
                quantity = product.quantity,
                categoryId = product.categoryId,
                categoryName = product.category != null ? product.category.name : string.Empty,
                offerName = offerName
            };
        }
        public static Product toModel(this addProductViewModel addproductViewModel)
        {
            return new Product
            {
                name = addproductViewModel.name,
                description = addproductViewModel.description,
                price = addproductViewModel.price,
                quantity = addproductViewModel.quantity,
                categoryId = addproductViewModel.categoryId,
                imageUrl = addproductViewModel.imageUrl ?? string.Empty

            };

        }
       
    }
}