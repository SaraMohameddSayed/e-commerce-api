using Managers.Events;
using Managers.Interfaces;
using Models;
using Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Managers.Handlers
{
    public class NewOrderAddedNotificationHandler:IEventHandler<NewOrderAddedEvent>
    {
        private notificationManager notificationManager;
        public NewOrderAddedNotificationHandler(notificationManager _notificationManager) {
        notificationManager=_notificationManager;
        }
        public async Task Handle(NewOrderAddedEvent @event)
        {
            var notification = new Notification
            {
                UserId = @event.UserId,
                Type = NotificationType.Order,
                SubType = NotificationSubType.NewOrder,
                EntityType = "Order",
                EntityId = @event.OrderId,
                Title = "Order Added",
                Message = $"New Order #{@event.OrderId} Added.",
                IsRead = false,
                CreatedAt = DateTime.UtcNow
            };
            await notificationManager.Add(notification);
        }

    }
}
