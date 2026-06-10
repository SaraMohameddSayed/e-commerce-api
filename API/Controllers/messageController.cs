using Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Domain;
using System.Security.Claims;
using DTOs;

namespace Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : BaseController
    {
        public MessageService _messageService;
        public MessageController(MessageService messageService)
        {
            _messageService = messageService;
        }

        [HttpPost]
        public async Task<IActionResult> submitMessage([FromForm] Message _message)
        {
            var userId=User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            _message.UserId = userId;

            if (userId == null)
            {
                _message.UserId = null;
               
               return Ok( await _messageService.Add(_message));
            }
            var userEmail = User.FindFirst(ClaimTypes.Email)?.Value;
            //معنى كدا ان اليوزر مسجل دخول
            _message.Email = userEmail;
            var result =  await _messageService.Add(_message);

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

            var result = _messageService.GetAll().ToList();
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
