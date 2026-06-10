using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class ProductWithOfferResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public string CategoryName { get; set; }
        public string CategoryId { get; set; }
        public int Quantity { get; set; }
        public int OfferId { get; set; }
        public string OfferName { get; set; }
        public decimal DiscountValue { get; set; }
        public decimal OriginalPrice { get; set; }
        public decimal DiscountedPrice { get; set; }
        public DateTime ApplicationDate { get; set; }
    }
}
