using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure;
using Domain;
namespace Services
{
    public class NotificationService:MainService<Notification>
    {
        public dbContext _context;
        public NotificationService(dbContext context):base(context){
            _context = context;
        }
        public List<Notification> GetUserNotifications(string userId)
        {
            return _context.Notification.Where(n => n.UserId == userId).ToList();
        }
        public async Task MarkAllAsRead(string userId)
        {
            var notifications = _context.Notification.Where(n => n.UserId == userId && !n.IsRead).ToList();
            foreach (var notification in notifications)
            {
                notification.IsRead = true;
            }
            await _context.SaveChangesAsync();
        }
    }
}
