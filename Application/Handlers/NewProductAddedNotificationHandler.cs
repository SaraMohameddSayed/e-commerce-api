using Services.Events;
using Services.Interfaces;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Handlers
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