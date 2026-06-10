using Application.Abstractions;
using Application.Events;
using Domain;
using Domain.Enums;
using Application.Services;

namespace Application.Handlers
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
