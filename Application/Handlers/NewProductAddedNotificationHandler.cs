using Application.Events;
using Application.Services;
using Domain;
using Application.Abstractions;

namespace Application.Handlers
{
    public class NewProductAddedNotificationHandler : IEventHandler<NewProductAddedEvent>
    {
        public NotificationService notificationManager;
        public NewProductAddedNotificationHandler(NotificationService _notificationManager)
        {
            notificationManager = _notificationManager;
        }
        public async Task Handle(NewProductAddedEvent @event)
        {

            foreach (var userId in @event.UsersId)
            {

                var notification = new Notification
                {
                    UserId = userId,
                    Type = Domain.Enums.NotificationType.Promo,
                    SubType = Domain.Enums.NotificationSubType.NewProduct,
                    EntityType = "Product",
                    EntityId = @event.ProductId,
                    Title = "New Product Added",
                    Message = $"A new product with ID {@event.ProductId} has been added.",
                    IsRead = false,
                    CreatedAt = DateTime.UtcNow
                };
                await notificationManager.Add(notification);
            }
        }
    }
}