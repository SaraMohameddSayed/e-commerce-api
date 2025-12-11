using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels
{
    public static class cartProductExtensions
    {
        public static CartProduct toModel(this addToCartViewModel _addToCartViewModel,int cartId)
        {
            return new CartProduct
            {
                cartId = cartId,
                productId = _addToCartViewModel.productId,
                quantity = _addToCartViewModel.quantity
            };
        }
        public static cartProductViewModel toCartProductViewModel(this CartProduct cartProduct)
        {
            return new cartProductViewModel
            {
                id = cartProduct.id,
                productId = cartProduct.productId,
                imageUrl = cartProduct.product.imageUrl,
                quantity = cartProduct.quantity,
                productName = cartProduct.product.name,
                productPrice = cartProduct.product.price,
                discountedPrice = cartProduct.product?.offers?
                             .OrderByDescending(o => o.applicationDate)?
                             .Select(o => (cartProduct.product.price - o.discountValue))?
                             .FirstOrDefault()?? cartProduct.product.price
            };
                    
        }
    }
}
