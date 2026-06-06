using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTOs;

namespace DTOs
{
    public class governorateViewModel
    {
        public int id { get; set; }
        public string name { get; set; }
        public bool isActive { get; set; }
        public List<areaViewModel>? areas { get; set; }

    }
}
