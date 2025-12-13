using Managers;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using System.Security.Claims;
using ViewModels;

namespace Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class cartController : ControllerBase
    {
        public cartManager cartManager;
        public cartProductManager cartProductManager;
        public productManager productManager;
        public UserManager<IdentityUser> userManager;
        public productOfferManager productOfferManager;

        public cartController(cartManager _cartManager, productManager _productManager, cartProductManager _cartProductManager,UserManager<IdentityUser> _userManager,productOfferManager _productOfferManager)
        {
            cartManager = _cartManager;
            productManager = _productManager;
            cartProductManager = _cartProductManager;
            userManager = _userManager; 
            productOfferManager = _productOfferManager;
        }
         [HttpPost]
        public async Task<IActionResult> addToCart(addToCartViewModel addToCart)
        {
           
            try
            {
                var userId= User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var cartId = await cartManager.getAll().Where(c => c.userId == userId)
                    .Select(c => c.id)
                    .FirstOrDefaultAsync();
                if (cartId == 0)
                {

                    await cartManager.Add(new Cart { userId = userId });
                    var newCart = await cartManager.getAll().Where(c => c.userId == userId).FirstOrDefaultAsync();
                    cartId =newCart.id;
                }

                var result = await cartProductManager.Add(addToCart.toModel(cartId));
                return Ok(result);
            }
            catch
            {
                throw;
            }
        }
        [HttpGet]
        public async Task<IActionResult> getCartProducts()
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var cartId = await cartManager.getAll().Where(c => c.userId == userId)
                    .Select(c => c.id)
                    .FirstOrDefaultAsync();
               

                var cartProducts = await cartProductManager.getAll()
                    .Where(cp => cp.cartId == cartId)
                    .Include(cp => cp.product)
                    .ThenInclude(p => p.offers)
                    .Select(cp => cp.toCartProductViewModel())
                    .ToListAsync();
                return Ok(cartProducts);
            }
            catch
            {
                throw;
            }
        }
        [HttpDelete]
        public async Task<IActionResult> removeFromCart(int cartProductId)
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var cartId = await cartManager.getAll().Where(c => c.userId == userId)
                    .Select(c => c.id)
                    .FirstOrDefaultAsync();
                var cartProduct = await cartProductManager.getAll()
                    .FirstOrDefaultAsync(cp => cp.id == cartProductId && cp.cartId==cartId);
                var result = await cartProductManager.Delete(cartProduct);
                return Ok(result);
            }
            catch
            {
                throw;
            }
        }
        [HttpPut]
        public async Task<IActionResult> updateCart(List<CartProduct> updatedcartProducts)
        {
            var userId= User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var cartId = await cartManager.getAll().Where(c => c.userId == userId)
                .Select(c => c.id)
                .FirstOrDefaultAsync();
            var cartproducts = await cartProductManager.getAll()
                .Where(cp => cp.cartId == cartId)
                .ToListAsync();
            foreach (var updatedProduct in updatedcartProducts)
            {
                var existingProduct = cartproducts.FirstOrDefault(cp => cp.id == updatedProduct.id);
                if (existingProduct != null)
                {
                    existingProduct.quantity = updatedProduct.quantity;
                    await cartProductManager.Update(existingProduct);
                }
            }
                return Ok();
        }
    }
}