
using DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Application.Services;
namespace Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : BaseController
    {
        public AuthService _accountManager;
        public AuthController(AuthService accountManager)
        {
            _accountManager = accountManager;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromForm] RegisterRequest registerrequest)
        {
            try
            {
                var userId=User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var result = await _accountManager.Register(registerrequest);
                if (result.Succeeded)
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest(result);
                }
            }
            catch
            {
                throw;
            }
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromForm] LoginRequest loginrequest)
        {
            try
            {
                var result = await _accountManager.Login(loginrequest);
                return Ok(new { token= result });
            }
            catch
            {
                throw;
            }
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            try
            {
                _accountManager.Logout();
                return Ok();
            }
            catch
            {
                throw;
            }
        }

    }
}
