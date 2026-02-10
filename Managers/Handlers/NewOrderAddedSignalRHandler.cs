using Managers.Abstractions;
using Managers.Events;
using Managers.Interfaces;
using Shared.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Managers.Handlers
{
    public class NewOrderAddedSignalRHandler:IEventHandler<NewOrderAddedEvent>
    {
        private IRealtimeNotifier RealtimeNotifier;
        public NewOrderAddedSignalRHandler(IRealtimeNotifier _RealTimeNotifier)
        {
            RealtimeNotifier= _RealTimeNotifier;
        }
        public async Task Handle(NewOrderAddedEvent @event)
        {
            await RealtimeNotifier.NotifyUserAsync(@event.UserId, new
            {
                @event.OrderId,
                @event.OccurredAt
            },SignalREvents.NewOrderAdded);
        }

    }
}
