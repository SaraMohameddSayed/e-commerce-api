using Managers.Interfaces;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Managers.Abstractions;
using Shared.Contracts;
using Managers.Events;
namespace Managers.Handlers
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
