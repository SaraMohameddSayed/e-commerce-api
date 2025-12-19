using Infrastructure;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Managers
{
    public class orderProductManager:MainManager<OrderProduct>
    {
        public  orderProductManager(dbContext _dbContext):base(_dbContext)
        {

        }
        public  List<OrderProduct> GenerateOrderProductsFromCartProducts(List<CartProduct> cartProducts)
        {
            var orderProducts = new List<OrderProduct>();
            foreach (var cartProduct in cartProducts)
            {
                var orderProduct = new OrderProduct
                {
                    productId = cartProduct.productId,
                    productName = cartProduct.product.name,
                    productImageUrl = cartProduct.product.imageUrl,
                    quantity = cartProduct.quantity,
                    price = cartProduct.product.price // Assuming CartProduct has a navigation property to Product
                };
                orderProducts.Add(orderProduct);
            }
            return orderProducts;
        }
    }
}
