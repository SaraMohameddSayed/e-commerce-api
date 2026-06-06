using Services.Events;
using Services.Interfaces;
using Domain;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Services.Handlers
{
    public class OrderStatusChangedNotificationHandler: IEventHandler<OrderStatusChangedEvent>
    {
        public NotificationService notificationManager;
        public OrderStatusChangedNotificationHandler(NotificationService _notificationManager)
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
                EntityType="Order",
                EntityId = @event.orderId,
                Title = "Order Updated",
                Message = $"Order #{@event.orderId} status changed to {@event.newStatus}.",
                IsRead = false,
                CreatedAt = DateTime.UtcNow
            };
            await notificationManager.Add(notification);
        }
    }
}
