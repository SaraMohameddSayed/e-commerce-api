using Models;
using Models.Enums;
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

        public string customerId { get; set; }
        public string customerName { get; set; }
        public  List<orderproductViewModel>? products { get; set; }
        public OrderStatus status { get; set; } = OrderStatus.Pending;

        public string governorateName { get; set; }
        public string areaName { get; set; }
        public string address { get; set; }
        public string phone { get; set; }

        public bool isPaid { get; set; } = false;
        public PaymentMethod paymentMethod { get; set; } 

        public decimal totalAmount { get; set; }
        public decimal subTotal { get; set; }
        public decimal deliveryFee { get; set; }

        public string? notes { get; set; }

        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }
        public string trackingNumber { get; set; }

    }
}
