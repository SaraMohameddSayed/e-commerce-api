using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTOs;

namespace DTOs
{
    public class GovernorateResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public List<AreaResponse>? Areas { get; set; }

    }
}
