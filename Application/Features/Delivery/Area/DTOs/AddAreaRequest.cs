using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class AddAreaRequest
    {
        public string name { get; set; }
        public decimal deliveryFee { get; set; }
        public int governorateId { get; set; }
    }
}
