using Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Domain;
using System.Security.Claims;
using DTOs;

namespace Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class messageController : BaseController
    {
        public MessageService messageManager;
        public messageController(MessageService _messageManager)
        {
            messageManager = _messageManager;
        }

        [HttpPost]
        public async Task<IActionResult> addMessage([FromForm] Message _message)
        {
            var userId=User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            _message.userId = userId;

            if (userId == null)
            {
                _message.userId = null;
               
               return Ok( await messageManager.Add(_message));
            }
            var userEmail = User.FindFirst(ClaimTypes.Email)?.Value;
            //معنى كدا ان اليوزر مسجل دخول
            _message.email = userEmail;
            var result =  await messageManager.Add(_message);

            if (result)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }

        }

        [HttpGet]

        public IActionResult getAllmessages()
        {

            var result = messageManager.getAll().ToList();
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
