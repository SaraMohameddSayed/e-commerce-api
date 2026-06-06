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
    public class NewOfferAddedNotificationHandler : IEventHandler<NewOfferAddedEvent>
    {
        public NotificationService notificationManager;
        public NewOfferAddedNotificationHandler(NotificationService _notificationManager)
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
                    Type = Domain.Enums.NotificationType.Promo,
                    SubType = Domain.Enums.NotificationSubType.NewOffer,
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

