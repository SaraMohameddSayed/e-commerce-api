using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class RegisterRequest
    {
        public string userName {get; set;}
        public string email { get; set; }
        public string password { get; set; }
    }
}
