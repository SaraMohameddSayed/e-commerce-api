using Services.Interfaces;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Services.Abstractions;
using Shared.Contracts;
using Services.Events;
namespace Services.Handlers
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
