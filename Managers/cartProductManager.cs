
using Infrastructure;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Managers
{
    public class cartProductManager:MainManager<CartProduct>
    {
        public cartProductManager(dbContext _dbContext) : base(_dbContext)
        {

        }
        public async Task<bool> clearCartByUserId(string userId)
        {
            var cartProducts = getAll().Where(cp => cp.cart.userId == userId);
            try
            {
                dbContext.Set<CartProduct>().RemoveRange(cartProducts);
                await dbContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                throw;
            }
        
        }
    }
}
