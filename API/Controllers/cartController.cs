using Application.Services;
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
        public CartService _cartService;
        public CartProductService _cartProductService;
        public ProductService _productService;
        public UserManager<IdentityUser> _userManager;
        public ProductOfferService _productOfferService;

        public cartController(CartService cartService, ProductService productService, CartProductService cartProductService,UserManager<IdentityUser> userManager,ProductOfferService productOfferService)
        {
            _cartService = cartService;
            _productService = productService;
            _cartProductService = cartProductService;
            _userManager = userManager; 
            _productOfferService = productOfferService;
        }
         [HttpPost]
        public async Task<IActionResult> addToCart(AddToCartRequest addToCartRequest)
        {
           
            try
            {
                var userId= User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var cartId = await _cartService.GetAll().Where(c => c.UserId == userId)
                    .Select(c => c.Id)
                    .FirstOrDefaultAsync();
                if (cartId == 0)
                {

                    await _cartService.Add(new Cart { UserId = userId });
                    var newCart = await _cartService.GetAll().Where(c => c.UserId == userId).FirstOrDefaultAsync();
                    cartId =newCart.Id;
                }

                var result = await _cartProductService.Add(addToCartRequest.ToCartProduct(cartId));
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
                var cartId = await _cartService.GetAll().Where(c => c.UserId == userId)
                    .Select(c => c.Id)
                    .FirstOrDefaultAsync();
               

                var cartProducts = await _cartProductService.GetAll()
                    .Where(cp => cp.CartId == cartId)
                    .Include(cp => cp.Product)
                    .ThenInclude(p => p.Offers)
                    .Select(cp => cp.ToResponse())
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
                var cartId = await _cartService.GetAll().Where(c => c.UserId == userId)
                    .Select(c => c.Id)
                    .FirstOrDefaultAsync();
                var cartProduct = await _cartProductService.GetAll()
                    .FirstOrDefaultAsync(cp => cp.Id == cartProductId && cp.CartId==cartId);
                var result = await _cartProductService.Delete(cartProduct);
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
            var cartId = await _cartService.GetAll().Where(c => c.UserId == userId)
                .Select(c => c.Id)
                .FirstOrDefaultAsync();
            var cartproducts = await _cartProductService.GetAll()
                .Where(cp => cp.CartId == cartId)
                .ToListAsync();
            foreach (var updatedProduct in updatedcartProducts)
            {
                var existingProduct = cartproducts.FirstOrDefault(cp => cp.Id == updatedProduct.Id);
                if (existingProduct != null)
                {
                    existingProduct.Quantity = updatedProduct.Quantity;
                    await _cartProductService.Update(existingProduct);
                }
            }
                return Ok();
        }
    }
}