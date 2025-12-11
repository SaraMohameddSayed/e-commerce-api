using ViewModels;
using Managers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class accountController : ControllerBase
    {
        public accountManager accountManager;
        public accountController(accountManager _accountManager)
        {
            accountManager = _accountManager;
        }

        [HttpPost("register")]
        public async Task<IActionResult> register([FromForm] registerViewModel registerViewModel)
        {
            try
            {
                var userId=User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var result = await accountManager.register(registerViewModel);
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
        public async Task<IActionResult> login([FromForm] loginViewModel loginViewModel)
        {
            try
            {
                var result = await accountManager.login(loginViewModel);
                return Ok(new { token= result });
            }
            catch
            {
                throw;
            }
        }

        [HttpPost("logout")]
        public IActionResult logout()
        {
            try
            {
                accountManager.logout();
                return Ok();
            }
            catch
            {
                throw;
            }
        }

    }
}
