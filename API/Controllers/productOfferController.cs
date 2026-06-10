using Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DTOs;

namespace Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductOfferController : BaseController
    {

        public ProductOfferService _productOfferService;
        public OfferService _offerService;
        public ProductService _productService;

        public ProductOfferController(ProductOfferService productOfferService, ProductService productService, OfferService offerService)
        {
            _productOfferService = productOfferService;
            _productService = productService;
            _offerService = offerService;
        }

        [HttpPost("addProductToOffer")]
        public async Task<IActionResult> addProductToOffer([FromForm] AddProductToOfferRequest addProductToOfferRequest)
        {

            var product = await _productService.GetOne(addProductToOfferRequest.ProductId);
            var offer = await _offerService.GetOne(addProductToOfferRequest.OfferId);
            if(product == null || offer == null)
            {
                return BadRequest("Invalid product or offer ID.");
            }
            var discountValue = (product.Price * offer.Discount) / 100;
            var result = await _productOfferService.Add(addProductToOfferRequest.ToProductOffer(discountValue));
            if (result)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }



        [HttpGet("getAllProductsWithOffers")]
        public IActionResult getAllProductsWithOffers()
        {

            var result = _productOfferService.GetAll()
                .Include(p => p.Product)
                .Include(p => p.Offer)
                .Include(p => p.Product!.Category)
                .Select(po=>po.ToResponse());
            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }
    }
}
