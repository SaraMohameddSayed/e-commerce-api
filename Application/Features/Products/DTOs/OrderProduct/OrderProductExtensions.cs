using System;
using Domain;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public static class OrderProductExtensions
    {
        public static OrderProductResponse ToResponse(this OrderProduct orderProduct)
        {

            return new OrderProductResponse
            {
                OrderId = orderProduct.OrderId,
                ProductName = orderProduct.ProductName,
                ProductId = orderProduct.ProductId,
                Quantity = orderProduct.Quantity,
                ImageUrl = orderProduct.ProductImageUrl,
                Price = orderProduct.Price
            };
        }
    }
}
