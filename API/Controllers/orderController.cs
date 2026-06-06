using Humanizer;
using Infrastructure;
using Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Domain;
using Domain.Enums;
using System.Security.Claims;
using DTOs;

namespace Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class orderController : BaseController
    {
        public OrderService orderManager;
        public ProductService productManager;
        public orderProductManager orderProductManager;
        public cartProductManager cartProductManager;
        public UserManager<IdentityUser> userManager;
        public orderController(OrderService _orderManager, ProductService _productManager, orderProductManager _orderProductManager, cartProductManager _cartProductManager,UserManager<IdentityUser> _userManager)
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
        [Authorize(Roles = "Admin")]
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
                    .Include(o => o.products)
                        .ThenInclude(o=>o.product)
                    .Select(o =>o.toViewModel()
                    )
                    .ToList()
            };

            return Ok(result);
        }

        [HttpGet]

        public async Task<IActionResult> getAllOrders(string? trackingNumber,OrderStatus? orderStatus,int pageNumber=1, int pageSize = 9)
        {
            
            var result =await orderManager.GetPagedOrders(trackingNumber,orderStatus,pageNumber,pageSize);
            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{orderId}/status")]
        public async Task<IActionResult> updateOrderStatus(int orderId, [FromBody] OrderStatus newStatus)
        {
        
            var result = await orderManager.updateOrderStatus(orderId, newStatus);
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