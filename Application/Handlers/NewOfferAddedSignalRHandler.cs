using Services.Abstractions;
using Services.Events;
using Shared.Contracts;
using Services.Events;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Handlers
{
    public class NewOfferAddedSignalRHandler: IEventHandler<NewOfferAddedEvent>
    {
        public IRealtimeNotifier RealtimeNotifier;
        public NewOfferAddedSignalRHandler(IRealtimeNotifier _RealTimeNotifier)
        {
            RealtimeNotifier = _RealTimeNotifier;
        }
        public async Task Handle(NewOfferAddedEvent @event)
        {
            foreach (var userId in @event.UsersId)
            {

                await RealtimeNotifier.NotifyUserAsync(userId, new
                {
                    @event.OfferId,
                    @event.OccurredAt
                }, SignalREvents.NewOfferAdded);
            }
        }
    }
}
