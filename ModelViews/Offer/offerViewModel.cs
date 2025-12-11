using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels
{
    public class offerViewModel
    {
        public int id { get; set; }
        public string name { get; set; }
        public double discount { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
    }
}
