using Humanizer;
using Managers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.Enums;
using System.Security.Claims;
using ViewModels;

namespace Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class orderController : ControllerBase
    {
        public orderManager orderManager;
        public productManager productManager;
        public orderProductManager orderProductManager;
        public cartProductManager cartProductManager;
        public orderController(orderManager _orderManager, productManager _productManager, orderProductManager _orderProductManager, cartProductManager _cartProductManager)
        {
            orderManager = _orderManager;
            productManager = _productManager;
            orderProductManager = _orderProductManager;
            cartProductManager = _cartProductManager;
        }


        [HttpPost]
        public async Task<IActionResult> addOrder(addOrderViewModel addorderViewModel)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var cartProducts = await cartProductManager.getAll().Where(cp => cp.cart.userId == userId).ToListAsync();
            var order = new Order
            {
                userId = userId,
                country = addorderViewModel.Country,
                city = addorderViewModel.City,
                address = addorderViewModel.Address,
                phone = addorderViewModel.Phone,
                paymentMethod = addorderViewModel.PaymentMethod,
                notes = addorderViewModel.Notes,
                status = OrderStatus.Pending,
                createdAt = DateTime.Now,
                updatedAt = DateTime.Now,
                trackingNumber = $"ORD-{DateTime.UtcNow:yyMMddHHmmss}",
                products = new List<OrderProduct>()
            };
            decimal totalAmount = 0;
            foreach (var cartProduct in cartProducts)
            {
                var product = await productManager.getOne(cartProduct.productId);

                decimal discountValue = product.offers?
                    .OrderByDescending(o => o.applicationDate)
                    .FirstOrDefault()?.discountValue ?? 0;

                decimal discountedPrice = discountValue > 0 ? product.price - discountValue : product.price;

                order.products.Add(new OrderProduct
                {
                    productId = cartProduct.productId,
                    quantity = cartProduct.quantity,
                    price = discountedPrice
                });

                // ÎÕã ÇáßãíÉ ãä ÇáãÎÒæä
                if (product != null)
                {
                    product.quantity -= cartProduct.quantity;
                }
                // ÊÍÏíË ÅÌãÇáí ÇáØáÈ
                totalAmount += discountedPrice * cartProduct.quantity;
            }
            order.totalAmount = totalAmount;
            var result = await orderManager.Add(order);

            if (result == false)
            {
                return BadRequest(new { message = "Failed to create order" });
            }
            // ÍÐÝ ÇáãäÊÌÇÊ ãä ÇáÓáÉ ÈÚÏ ÅäÔÇÁ ÇáØáÈ
            foreach (var cartProduct in cartProducts)
            {
                await cartProductManager.Delete(cartProduct);
            }

            return Ok(new { message = "Order created successfully", orderId = order.id });

        }




        [HttpGet("my-orders")]

        public IActionResult getAllOrdersByUserId()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var result = orderManager.getAll().Where(o=>o.userId==userId).Include(o=>o.products).Select(o=>o.toViewModel()).ToListAsync();
            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }
        [HttpGet("{orderId}")]
        public async Task<IActionResult> getByOrderId(int orderId)
        {
            var order = await orderManager.getOne(orderId);

            return Ok(order.toViewModel());
        }

        //Admin
        [HttpGet]

        public IActionResult getAllOrders()
        {

            var result = orderManager.getAll().Include(o => o.products).Select(o => o.toViewModel()).ToListAsync();
            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        [HttpGet("status/{status}")]
        public async Task<IActionResult> getOrdersByStatus(OrderStatus status)
        {
            var orders = await orderManager.getAll().Where(o => o.status == status).ToListAsync();
            return Ok(orders.Select(o => o.toViewModel()));
        }
        [HttpPut("{orderId}/status")]
        public async Task<IActionResult> updateOrderStatus(int orderId, [FromBody] OrderStatus newStatus)
        {
            var order = await orderManager.getOne(orderId);
            if (order == null)
            {
                return NotFound(new { message = "Order not found" });
            }
            order.status = newStatus;
            order.updatedAt = DateTime.Now;
            var result = await orderManager.Update(order);
            if (result)
            {
                return Ok(new { message = "Order status updated successfully" });
            }
            else
            {
                return BadRequest(new { message = "Failed to update order status" });
            }
        }
    }
}