using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels
{
    public class orderViewModel
    {

        public int id { get; set; }

        public string userId { get; set; }

        public  List<orderproductViewModel>? products { get; set; }

        public orderStatus status { get; set; } = orderStatus.Pending;

        public string country { get; set; }
        public string city { get; set; }
        public string address { get; set; }
        public string phone { get; set; }

        public bool isPaid { get; set; } = false;
        public string paymentMethod { get; set; }  // cod / card

        public decimal totalAmount { get; set; }

        public string? notes { get; set; }

        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }
        public string trackingNumber { get; set; }

    }
}
