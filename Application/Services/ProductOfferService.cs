using Infrastructure;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services;
{
    public class ProductOfferService : MainService<ProductOffer>
    {
        public ProductOfferService(AppDbContext AppDbContext) : base(AppDbContext)
        {
           
        }
    }

}
