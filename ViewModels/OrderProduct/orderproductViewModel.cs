using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels
{
    public class orderproductViewModel
    {
        public int orderId { get; set; }
        public int productId { get; set; }
        public string productName { get; set; }
        public string imageUrl { get; set; }
        public decimal price { get; set; }

        public int quantity { get; set; }
    }
}
