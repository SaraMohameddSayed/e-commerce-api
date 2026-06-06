using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Events
{
    public class OrderStatusChangedEvent
    {
        public string userId { get; set; }
        public int orderId { get; set; }
        public string newStatus { get; set; }
        public DateTime OccurredAt { get; set; }
        public OrderStatusChangedEvent(string userId, int orderId, string newStatus)
        {
            this.userId = userId;
            this.orderId = orderId;
            this.newStatus = newStatus;
            this.OccurredAt = DateTime.UtcNow;
        }
    }
}
