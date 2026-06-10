

namespace Application.Events
{
    public class NewProductAddedEvent
    {
        public List<string> UsersId { get; set; }
        public int ProductId { get; set; }
        public DateTime OccurredAt { get; set; }
        public NewProductAddedEvent(List<string> usersId, int _productId)
        {
            UsersId = usersId;
            ProductId = _productId;
            OccurredAt = DateTime.UtcNow;
        }
    }
}
