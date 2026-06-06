using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Services.Events;
using Services.Interfaces;
using Services.Abstractions;
using Shared.Contracts;

namespace Services.Handlers
{
    public class NewProductAddedSignalRHandler:IEventHandler<NewProductAddedEvent>
    {
        public IRealtimeNotifier RealtimeNotifier;
        public NewProductAddedSignalRHandler(IRealtimeNotifier _RealTimeNotifier)
        {
            RealtimeNotifier= _RealTimeNotifier;
        }
        public async Task Handle(NewProductAddedEvent @event)
        {
            foreach (var userId in @event.UsersId)
            {

                await RealtimeNotifier.NotifyUserAsync(userId, new
                {
                    @event.ProductId,
                    @event.OccurredAt
                }, SignalREvents.NewProductAdded);
            }
        }
    }
}
