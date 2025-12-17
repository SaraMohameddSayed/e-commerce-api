
using Models;
using Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels
{
    public class addOrderViewModel
    {
        public string? userId { get; set; }
        public int governorateId { get; set; }
        public int areaId { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public decimal TotalAmount { get; set; }
        public string Notes { get; set; }


    }
}
