using Services.Abstractions;
using Services.Events;
using Services.Interfaces;
using Shared.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Handlers
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
