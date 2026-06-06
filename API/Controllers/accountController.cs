
using DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Services;
namespace Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : BaseController
    {
        public AccountService _accountManager;
        public AccountController(AccountService accountManager)
        {
            _accountManager = accountManager;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromForm] RegisterRequest registerViewModel)
        {
            try
            {
                var userId=User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var result = await _accountManager.Register(registerViewModel);
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
        public async Task<IActionResult> Login([FromForm] LoginRequest loginViewModel)
        {
            try
            {
                var result = await _accountManager.Login(loginViewModel);
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
