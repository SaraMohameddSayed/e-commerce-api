using Controllers;
using Services;
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
      public NotificationService notificationManager;
        public NotificationsController(NotificationService _notificationManager)
        {
            notificationManager = _notificationManager;
        }

        [HttpGet]
        public IActionResult getUserNotifications()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var notifications = notificationManager.getUserNotifications(userId);
            return Ok(notifications);
        }
        [HttpPost("markAllAsRead")]
        public async Task<IActionResult> markAllAsRead()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            await notificationManager.markAllAsRead(userId);
            return Ok();
        }

    }
}
