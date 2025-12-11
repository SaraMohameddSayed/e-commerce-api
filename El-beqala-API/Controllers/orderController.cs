using Managers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models;
using System.Security.Claims;

namespace Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class orderController : ControllerBase
    {
        public orderManager orderManager;
        public productManager productManager;
        public orderController(orderManager _orderManager, productManager _productManager)
        {
            orderManager=_orderManager;
            productManager = _productManager;
        }


        [HttpPost]
        public async Task<IActionResult> addOrder( List<int> productIds)
        {
            var userId= User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var products=productManager.getAll().Where(p=> productIds.Contains( p.id)).ToList();
            var order=new Order
            {
                userId=userId,
                products= products
            };
            var result =await orderManager.Add(order);
            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
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