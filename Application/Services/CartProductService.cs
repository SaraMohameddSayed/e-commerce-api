
using Infrastructure;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class CartProductService:MainService<CartProduct>
    {
        public CartProductService(dbContext _dbContext) : base(_dbContext)
        {

        }
        public async Task<bool> ClearCartByUserId(string userId)
        {
            var cartProducts = GetAll().Where(cp => cp.cart.userId == userId);
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
