using Services;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Domain;
using System.Security.Claims;
using DTOs;

namespace Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class cartController : BaseController
    {
        public CartManager _cartManager;
        public CartProductManager _cartProductManager;
        public ProductManager _productManager;
        public UserManager<IdentityUser> _userManager;
        public ProductOfferManager _productOfferManager;

        public cartController(CartService cartManager, ProductService productManager, cartProductManager cartProductManager,UserManager<IdentityUser> userManager,productOfferManager productOfferManager)
        {
            _cartManager = cartManager;
            _productManager = productManager;
            _cartProductManager = cartProductManager;
            _userManager = userManager; 
            _productOfferManager = productOfferManager;
        }
         [HttpPost]
        public async Task<IActionResult> addToCart(AddToCartRequest addToCart)
        {
           
            try
            {
                var userId= User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var cartId = await _cartManager.GetAll().Where(c => c.userId == userId)
                    .Select(c => c.id)
                    .FirstOrDefaultAsync();
                if (cartId == 0)
                {

                    await _cartManager.Add(new Cart { userId = userId });
                    var newCart = await _cartManager.GetAll().Where(c => c.userId == userId).FirstOrDefaultAsync();
                    cartId =newCart.id;
                }

                var result = await _cartProductManager.Add(addToCart.toModel(cartId));
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
                var cartId = await _cartManager.GetAll().Where(c => c.userId == userId)
                    .Select(c => c.id)
                    .FirstOrDefaultAsync();
               

                var cartProducts = await _cartProductManager.GetAll()
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
                var cartId = await _cartManager.GetAll().Where(c => c.userId == userId)
                    .Select(c => c.id)
                    .FirstOrDefaultAsync();
                var cartProduct = await _cartProductManager.GetAll()
                    .FirstOrDefaultAsync(cp => cp.id == cartProductId && cp.cartId==cartId);
                var result = await _cartProductManager.Delete(cartProduct);
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
            var cartId = await _cartManager.GetAll().Where(c => c.userId == userId)
                .Select(c => c.id)
                .FirstOrDefaultAsync();
            var cartproducts = await _cartProductManager.GetAll()
                .Where(cp => cp.cartId == cartId)
                .ToListAsync();
            foreach (var updatedProduct in updatedcartProducts)
            {
                var existingProduct = cartproducts.FirstOrDefault(cp => cp.id == updatedProduct.id);
                if (existingProduct != null)
                {
                    existingProduct.quantity = updatedProduct.quantity;
                    await _cartProductManager.Update(existingProduct);
                }
            }
                return Ok();
        }
    }
}