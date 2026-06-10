using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Domain.Enums;
using System.Security.Claims;
using DTOs;
using Application.Shared.Common;
namespace Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class OrderController : BaseController
    {
        public OrderService _orderService;
        public ProductService _productService;
        public OrderProductService _orderProductService;
        public CartProductService _cartProductService;
        public UserManager<IdentityUser> _userManager;
        public OrderController(OrderService orderService, ProductService productService, OrderProductService orderProductService, CartProductService cartProductService, UserManager<IdentityUser> userManager)
        {
            _orderService = orderService;
            _productService = productService;
            _orderProductService = orderProductService;
            _cartProductService = cartProductService;
            _userManager = userManager;

        }


        [HttpPost]
        public async Task<IActionResult> addOrder(AddOrderRequest addorderRequest)
        {
            addorderRequest.UserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value; 
            var order = await _orderService.CreateOrderFromCart(addorderRequest);

            if (order == null)
            {
                return BadRequest(new { message = "Failed to create order" });
            }
           

            return Ok(new { message = "Order created successfully", orderId = order.Id });

        }




        [HttpGet("my-orders")]

        public IActionResult getAllOrdersByUserId()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var result = _orderService.GetAll().Where(o=>o.UserId==userId).Include(o=>o.Products).Select(o=>o.ToResponse()).ToListAsync();
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
            var order = await _orderService.GetOne(orderId);

            return Ok(order.ToResponse());
        }

        //Admin
        [Authorize(Roles = "Admin")]
        [HttpGet("dashboard")]
        public IActionResult OrdersDashboard()
        {
            var orders = _orderService.GetAll();

            var result = new
            {
                totalOrders = orders.Count(),
                pendingOrders = orders.Count(o => o.Status == OrderStatus.Pending),
                confirmedOrders = orders.Count(o => o.Status == OrderStatus.Confirmed),
                shippedOrders = orders.Count(o => o.Status == OrderStatus.Shipped),
                deliveredOrders = orders.Count(o => o.Status == OrderStatus.Delivered),
                latestOrders = orders?
                    .OrderByDescending(o => o.CreatedAt)
                    .Take(5)
                    .Include(o=> o.User)
                    .Include(o => o.Products)
                        .ThenInclude(o=>o.Product)
                    .Select(o =>o.ToResponse()
                    )
                    .ToList()
            };

            return Ok(result);
        }

        [HttpGet]

        public async Task<IActionResult> getAllOrders(string? trackingNumber,OrderStatus? orderStatus,int pageNumber=1, int pageSize = 9)
        {
            
            var result =await _orderService.GetPagedOrders(trackingNumber,orderStatus,pageNumber,pageSize);
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
        
            var result = await _orderService.UpdateOrderStatus(orderId, newStatus);
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