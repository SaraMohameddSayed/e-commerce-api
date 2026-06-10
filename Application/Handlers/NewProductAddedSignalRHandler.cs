using Application.Events;
using Application.Abstractions;
using Application.Shared.Contracts;
namespace Application.Handlers
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
