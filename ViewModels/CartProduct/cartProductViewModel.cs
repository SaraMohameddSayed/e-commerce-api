using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels
{
    public class cartProductViewModel
    {
        public int id { get; set; }
        public int productId { get; set; }
        public string imageUrl { get; set; }
        public int quantity { get; set; }
        public string productName { get; set; }
        public decimal productPrice { get; set; }
        public decimal? discountedPrice { get; set; }

    }
}
