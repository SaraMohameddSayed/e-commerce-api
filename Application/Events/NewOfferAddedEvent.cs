
namespace Application.Events
{
    public class NewOfferAddedEvent
    {
        public List<string> UsersId { get; set; }
        public int OfferId { get; set; }
        public DateTime OccurredAt { get; set; }
        public NewOfferAddedEvent(List<string> usersId, int _offerId)
        {
            UsersId = usersId;
            OfferId = _offerId;
            OccurredAt = DateTime.UtcNow;
        }
    }
}
