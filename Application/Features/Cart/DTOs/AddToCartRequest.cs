using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class AddToCartRequest
    {
        public int productId { get; set; }
        public int quantity { get; set; }
    }
}
