using Managers.Events;
using Managers.Interfaces;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Managers.Handlers
{
    public class NewProductAddedNotificationHandler : IEventHandler<NewProductAddedEvent>
    {
        public notificationManager notificationManager;
        public NewProductAddedNotificationHandler(notificationManager _notificationManager)
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
                    Type = Models.Enums.NotificationType.Promo,
                    SubType = Models.Enums.NotificationSubType.NewProduct,
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