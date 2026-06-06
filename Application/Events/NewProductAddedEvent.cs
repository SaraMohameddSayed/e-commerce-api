using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Events
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
