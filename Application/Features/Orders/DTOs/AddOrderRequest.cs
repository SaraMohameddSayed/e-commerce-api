
using Domain;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class AddOrderRequest
    {
        public string? UserId { get; set; }
        public int GovernorateId { get; set; }
        public int AreaId { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public decimal TotalAmount { get; set; }
        public string Notes { get; set; }


    }
}
