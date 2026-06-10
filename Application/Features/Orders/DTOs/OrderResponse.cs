using Domain;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class OrderResponse
    {

        public int Id { get; set; }

        public string CustomerId { get; set; }
        public string CustomerName { get; set; }
        public  List<OrderProductResponse>? Products { get; set; }
        public OrderStatus Status { get; set; } = OrderStatus.Pending;

        public string GovernorateName { get; set; }
        public string AreaName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }

        public bool IsPaid { get; set; } = false;
        public PaymentMethod PaymentMethod { get; set; } 

        public decimal TotalAmount { get; set; }
        public decimal SubTotal { get; set; }
        public decimal DeliveryFee { get; set; }
        public string? Notes { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string TrackingNumber { get; set; }

    }
}
