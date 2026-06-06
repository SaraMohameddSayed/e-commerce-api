using Infrastructure;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ProductOfferService : MainService<ProductOffer>
    {
        public ProductOfferService(dbContext context) : base(context)
        {
           
        }
    }

}
