using Managers.Abstractions;
using Managers.Events;
using Shared.Contracts;
using Managers.Events;
using Managers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Managers.Handlers
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
