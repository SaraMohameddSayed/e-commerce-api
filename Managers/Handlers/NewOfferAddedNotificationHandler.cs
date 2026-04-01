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
    public class NewOfferAddedNotificationHandler : IEventHandler<NewOfferAddedEvent>
    {
        public notificationManager notificationManager;
        public NewOfferAddedNotificationHandler(notificationManager _notificationManager)
        {
            notificationManager = _notificationManager;
        }
        public async Task Handle(NewOfferAddedEvent @event)
        {

            foreach (var userId in @event.UsersId)
            {

                var notification = new Notification
                {
                    UserId = userId,
                    Type = Models.Enums.NotificationType.Promo,
                    SubType = Models.Enums.NotificationSubType.NewOffer,
                    EntityType = "Offer",
                    EntityId = @event.OfferId,
                    Title = "New Offer Added",
                    Message = $"A new offer with ID {@event.OfferId} has been added.",
                    IsRead = false,
                    CreatedAt = DateTime.UtcNow
                };
                await notificationManager.Add(notification);
            }
        }
    }
    
}

