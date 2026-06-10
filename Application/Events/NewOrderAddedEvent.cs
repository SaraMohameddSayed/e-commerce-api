
namespace Application.Events
{
    public class NewOrderAddedEvent
    {
        public string UserId { get; set; }
        public int OrderId { get; set; }
        public DateTime OccurredAt { get; set; }
        public NewOrderAddedEvent(string userId, int _orderId)
            {
            UserId = userId;
            OrderId = _orderId;
            OccurredAt = DateTime.UtcNow;
            }
    }
}
