using Managers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
        public orderController(orderManager _orderManager, productManager _productManager, orderProductManager _orderProductManager)
        {
            orderManager = _orderManager;
            productManager = _productManager;
            orderProductManager = _orderProductManager;
        }


        [HttpPost]
        public async Task<IActionResult> addOrder( List<addOrderProductViewModel> addorderproduct)
        {
            var userId= User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return Ok();
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