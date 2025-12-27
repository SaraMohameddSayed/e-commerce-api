using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels
{
    public class areaViewModel
    {
        public int id { get; set; }
        public string name { get; set; }
        public bool isActive { get; set; }
        public decimal deliveryFee { get; set; }
        public int governorateId { get; set; }
    }
}
