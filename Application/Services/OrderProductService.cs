using Infrastructure;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services;
{
    public class OrderProductService:MainService<OrderProduct>
    {
        public  OrderProductService(AppDbContext  AppDbContext ):base( AppDbContext )
        {

        }
        public  List<OrderProduct> GenerateOrderProductsFromCartProducts(List<CartProduct> cartProducts)
        {
            var orderProducts = new List<OrderProduct>();
            foreach (var cartProduct in cartProducts)
            {
                var orderProduct = new OrderProduct
                {
                    ProductId = cartProduct.ProductId,
                    ProductName = cartProduct.Product.Name,
                    ProductImageUrl = cartProduct.Product.ImageUrl,
                    Quantity = cartProduct.Quantity,
                    Price = cartProduct.Product.Price // Assuming CartProduct has a navigation property to Product
                };
                orderProducts.Add(orderProduct);
            }
            return orderProducts;
        }
    }
}
