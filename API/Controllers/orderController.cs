using Humanizer;
using Infrastructure;
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
        public UserManager<IdentityUser> userManager;
        public orderController(orderManager _orderManager, productManager _productManager, orderProductManager _orderProductManager, cartProductManager _cartProductManager,UserManager<IdentityUser> _userManager)
        {
            orderManager = _orderManager;
            productManager = _productManager;
            orderProductManager = _orderProductManager;
            cartProductManager = _cartProductManager;
            userManager = _userManager;

        }


        [HttpPost]
        public async Task<IActionResult> addOrder(addOrderViewModel addorderViewModel)
        {
            addorderViewModel.userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value; 
            var order = await orderManager.CreateOrderFromCart(addorderViewModel);

            if (order == null)
            {
                return BadRequest(new { message = "Failed to create order" });
            }
           

            return Ok(new { message = "Order created successfully", orderId = order.id });

        }




        [HttpGet("my-orders")]

        public IActionResult getAllOrdersByUserId()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var result = orderManager.getAll().Where(o=>o.userId==userId).Include(o=>o.products).Include(o=>o.governorate).ThenInclude(g=>g.areas).Select(o=>o.toViewModel()).ToListAsync();
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
        [HttpGet("dashboard")]
        public IActionResult OrdersDashboard()
        {
            var orders = orderManager.getAll();

            var result = new
            {
                totalOrders = orders.Count(),
                pendingOrders = orders.Count(o => o.status == OrderStatus.Pending),
                confirmedOrders = orders.Count(o => o.status == OrderStatus.Confirmed),
                shippedOrders = orders.Count(o => o.status == OrderStatus.Shipped),
                deliveredOrders = orders.Count(o => o.status == OrderStatus.Delivered),

                latestOrders = orders?
                    .OrderByDescending(o => o.createdAt)
                    .Take(5)
                    .Include(o=> o.user)
                    .Include(o => o.governorate)
                    .ThenInclude(g => g.areas)
                    .Include(o => o.products)
                    .ThenInclude(o=>o.product)
                    .Select(o =>o.toViewModel()
                    )
                    .ToList()
            };

            return Ok(result);
        }

        [HttpGet]

        public IActionResult getAllOrders()
        {

            var result = orderManager.getAll().Include(o=>o.user).Include(o => o.products).ThenInclude(o=>o.product).Include(o => o.governorate).ThenInclude(g => g.areas).Select(o => o.toViewModel());
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
            var orders = await orderManager.getAll().Where(o => o.status == status)
                .Include(o => o.user)
                .Include(o => o.governorate)
                    .ThenInclude(g => g.areas)
                 .Include(o => o.products)
                    .ThenInclude(o => o.product)
                .Select(o => o.toViewModel())
                .ToListAsync();
            return Ok(orders);
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