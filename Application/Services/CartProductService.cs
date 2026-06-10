
using Infrastructure;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class CartProductService:MainService<CartProduct>
    {
        private readonly AppDbContext _AppDbContext;
        public CartProductService(AppDbContext  AppDbContext ) : base(AppDbContext )
        {
            _AppDbContext = AppDbContext;
        }
        public async Task<bool> ClearCartByUserId(string userId)
        {
            var cartProducts = GetAll().Where(cp => cp.Cart.UserId == userId);
            try
            {
                _AppDbContext .Set<CartProduct>().RemoveRange(cartProducts);
                await _AppDbContext .SaveChangesAsync();
                return true;
            }
            catch
            {
                throw;
            }
        
        }
    }
}
