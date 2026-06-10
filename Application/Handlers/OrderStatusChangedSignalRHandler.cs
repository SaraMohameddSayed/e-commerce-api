using Application.Abstractions;
using Application.Events;
using Application.Shared.Contracts;
namespace Application.Handlers
{
    public class OrderStatusChangedSignalRHandler:IEventHandler<OrderStatusChangedEvent>
    {
        private readonly IRealtimeNotifier realtimeNotifier;

        public OrderStatusChangedSignalRHandler(
            IRealtimeNotifier _realtimeNotifier)
        {
            realtimeNotifier = _realtimeNotifier;
        }

        public async Task Handle(OrderStatusChangedEvent @event)
        {
            await realtimeNotifier.NotifyUserAsync(@event.userId, new
            {
                OrderId = @event.orderId,
                NewStatus = @event.newStatus
            }, SignalREvents.OrderStatusUpdated);
        }
    }

}
