using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure;
using Domain;
namespace Application.Services;
{
    public class NotificationService:MainService<Notification>
    {
        private readonly AppDbContext _AppDbContext;
        public NotificationService(AppDbContext AppDbContext) :base(AppDbContext)
        {
            _AppDbContext = AppDbContext;
        }
        public List<Notification> GetUserNotifications(string userId)
        {
            return _AppDbContext.Notification.Where(n => n.UserId == userId).ToList();
        }
        public async Task MarkAllAsRead(string userId)
        {
            var notifications = _AppDbContext.Notification.Where(n => n.UserId == userId && !n.IsRead).ToList();
            foreach (var notification in notifications)
            {
                notification.IsRead = true;
            }
            await _AppDbContext.SaveChangesAsync();
        }
    }
}
