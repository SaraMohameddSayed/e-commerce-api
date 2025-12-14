using System;
using Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels
{
    public static class orderProductExtensions
    {
        public static orderproductViewModel toViewModel(this OrderProduct orderProduct)
        {

            return new orderproductViewModel
            {
                orderId = orderProduct.orderId,
                productName= orderProduct.product != null ? orderProduct.product.name : string.Empty,
                productId = orderProduct.productId,
                quantity = orderProduct.quantity,
                imageUrl = orderProduct.product != null ? orderProduct.product.imageUrl : string.Empty,
                price = orderProduct.price
            };
        }
    }
}
