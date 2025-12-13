using Humanizer;
using Managers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
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
        public orderController(orderManager _orderManager, productManager _productManager, orderProductManager _orderProductManager,cartProductManager _cartProductManager)
        {
            orderManager = _orderManager;
            productManager = _productManager;
            orderProductManager = _orderProductManager;
            cartProductManager = _cartProductManager;
        }


        [HttpPost]
        public async Task<IActionResult> addOrder( addOrderViewModel addorderViewModel)
        {
            var userId= User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var cartProducts= await cartProductManager.getAll().Where(cp=>cp.cart.userId==userId).ToListAsync();
            var order = new Order
            {
                userId = userId,
                country = addorderViewModel.Country,
                city = addorderViewModel.City,
                address = addorderViewModel.Address,
                phone = addorderViewModel.Phone,
                paymentMethod = addorderViewModel.PaymentMethod,
                totalAmount = addorderViewModel.TotalAmount,
                notes = addorderViewModel.Notes,
                status = orderStatus.Pending,
                createdAt = DateTime.Now,
                updatedAt = DateTime.Now,
                trackingNumber = $"ORD-{DateTime.UtcNow:yyMMddHHmmss}",
                products = new List<OrderProduct>()
            };

            foreach (var cartProduct in cartProducts)
            {
                order.products.Add(new OrderProduct
                {
                   productId= cartProduct.productId,
                   quantity= cartProduct.quantity
                });

                // Œ’„ «·ﬂ„Ì… „‰ «·„Œ“Ê‰
                var product = await productManager.getOne(cartProduct.productId);
                if (product != null)
                {
                    product.quantity -= cartProduct.quantity;
                }
            }

          var result= await orderManager.Add(order);

           if(result== false)
            {
                return BadRequest(new { message = "Failed to create order" });
            }
            // Õ–› «·„‰ Ã«  „‰ «·”·… »⁄œ ≈‰‘«¡ «·ÿ·»
            foreach (var cartProduct in cartProducts)
            {
                await cartProductManager.Delete(cartProduct);
            }

            return Ok(new { message = "Order created successfully", orderId = order.id });

        }



        [HttpGet("getAll")]

        public IActionResult getAllOrders()
        {

            var result = orderManager.getAll().ToList();
            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }


        [HttpGet("getByUserId")]

        public IActionResult getAllOrdersByUserId(int _userId)
        {

            var result = orderManager.getOne(_userId);
            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }
    }
}