
using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        //Helper :يستخدم لتوحيد نوع النتيجة و سهولةالتحكم فيها 
       protected IActionResult ApiResult<T>(ApiResponse<T> apiRespose)
        {
            return StatusCode(apiRespose.StatusCode,apiRespose);
        }
    }
}
