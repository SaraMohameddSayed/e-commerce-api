using Controllers;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class NotificationsController : BaseController
    {
      public NotificationService _notificationService;
        public NotificationsController(NotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        [HttpGet]
        public IActionResult getUserNotifications()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var notifications = _notificationService.GetUserNotifications(userId);
            return Ok(notifications);
        }
        [HttpPost("markAllAsRead")]
        public async Task<IActionResult> markAllAsRead()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            await _notificationService.MarkAllAsRead(userId);
            return Ok();
        }

    }
}
