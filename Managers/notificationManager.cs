using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure;
using Models;
namespace Managers
{
    public class notificationManager:MainManager<Notification>
    {
        public dbContext context;
        public notificationManager(dbContext _dbcontext):base(_dbcontext){
            context = _dbcontext;
        }
        public List<Notification> getUserNotifications(string userId)
        {
            return context.Notification.Where(n => n.UserId == userId).ToList();
        }
        public async Task markAllAsRead(string userId)
        {
            var notifications = context.Notification.Where(n => n.UserId == userId && !n.IsRead).ToList();
            foreach (var notification in notifications)
            {
                notification.IsRead = true;
            }
            await context.SaveChangesAsync();
        }
    }
}
