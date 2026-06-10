using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class AddProductToOfferRequest
    {
        public int ProductId { get; set; }

        public int OfferId { get; set; }
        public DateTime ApplicationDate { get; set; }

    }
}
