using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class ProductWithOfferViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string imageUrl { get; set; }
        public string categoryName { get; set; }
        public string categoryId { get; set; }
        public int quantity { get; set; }
        public int offerId { get; set; }
        public string offerName { get; set; }
        public decimal discountValue { get; set; }
        public decimal originalPrice { get; set; }
        public decimal discountedPrice { get; set; }
        public DateTime applicationDate { get; set; }
    }
}
