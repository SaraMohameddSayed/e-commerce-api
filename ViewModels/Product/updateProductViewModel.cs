using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels
{
    public class updateProductViewModel
    {
        public int id { get; set; }
        public string name { get; set; }

        public decimal price { get; set; }

        public int quantity { get; set; }

        public IFormFile? imageFile { get; set; }
        public string? imageUrl { get; set; }

        public string? description { get; set; }

        public int categoryId { get; set; }
    }
}
