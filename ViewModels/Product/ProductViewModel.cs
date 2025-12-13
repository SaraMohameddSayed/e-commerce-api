using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels
{
    public class ProductViewModel
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public decimal originalPrice { get; set; }
        public decimal discountedPrice { get; set; }
        public string imageUrl { get; set; }
        public int quantity { get; set; }
        public int categoryId { get; set; }
        public string categoryName { get; set; }
        public string offerName { get; set; }
    }
}
