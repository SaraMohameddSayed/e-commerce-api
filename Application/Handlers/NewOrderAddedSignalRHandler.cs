using Application.Shared.Contracts;
using Application.Abstractions;
using Application.Events;


namespace Application.Handlers
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
