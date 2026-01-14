using Managers.Events;
using Managers.Interfaces;
using Models;
using Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Managers.Handlers
{
    public class OrderStatusChangedNotificationHandler: IEventHandler<OrderStatusChangedEvent>
    {
        public notificationManager notificationManager;
        public OrderStatusChangedNotificationHandler(notificationManager _notificationManager)
        {
            notificationManager = _notificationManager;
        }
        public async Task Handle(OrderStatusChangedEvent @event)
        {
            var notification = new Notification
            {
                UserId = @event.userId,
                Type = NotificationType.Order,
                SubType = NotificationSubType.OrderStatusChanged,
                Title = "Order Updated",
                Message = $"Order #{@event.orderId} status changed to {@event.newStatus}.",
                IsRead = false,
                CreatedAt = DateTime.UtcNow
            };
            await notificationManager.Add(notification);
        }
    }
}
