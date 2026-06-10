using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public static class CartProductExtensions
    {
        public static CartProduct ToCartProduct(this AddToCartRequest addToCartrequest,int cartId)
        {
            return new CartProduct
            {
                CartId = cartId,
                ProductId = addToCartrequest.ProductId,
                Quantity = addToCartrequest.Quantity
            };
        }
        public static CartProductResponse ToResponse(this CartProduct cartProduct)
        {
            return new CartProductResponse
            {
                Id = cartProduct.Id,
                ProductId = cartProduct.ProductId,
                ImageUrl = cartProduct.Product.ImageUrl,
                Quantity = cartProduct.Quantity,
                ProductName = cartProduct.Product.Name,
                ProductPrice = cartProduct.Product.Price,
                DiscountedPrice = cartProduct.Product?.Offers?
                             .OrderByDescending(o => o.ApplicationDate)?
                             .Select(o => (cartProduct.Product.Price - o.DiscountValue))?
                             .FirstOrDefault()?? cartProduct.Product.Price
            };
                    
        }
    }
}
