using Application.Abstractions;
using Application.Events;
using Application.Shared.Contracts;

namespace Application.Handlers
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
