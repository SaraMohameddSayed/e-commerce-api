using Application.Events;
using Application.Services;
using Domain;
using Domain.Enums;
using Application.Abstractions;


namespace Application.Handlers
{
    public class NewOrderAddedNotificationHandler:IEventHandler<NewOrderAddedEvent>
    {
        private NotificationService notificationManager;
        public NewOrderAddedNotificationHandler(NotificationService _notificationManager) {
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
