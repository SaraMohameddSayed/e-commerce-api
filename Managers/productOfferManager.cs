using Infrastructure;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Managers
{
    public class productOfferManager : MainManager<ProductOffer>
    {
        public dbContext dbContext { get; set; }

        public productOfferManager(dbContext _context) : base(_context)
        {
            dbContext = _context;
        }

        public async Task<bool> addProductToOffer(ProductOffer _productOffers)
        {
            try
            {
                await dbContext.Set<ProductOffer>().AddAsync(_productOffers);
                await dbContext.SaveChangesAsync();
                return true;

            }
            catch (Exception ex)
            {
                throw;
            }
        }


    }

}
