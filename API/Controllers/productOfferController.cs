using Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DTOs;

namespace Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class productOfferController : BaseController
    {

        public productOfferManager productOfferManager;
        public OfferService offerManager;
        public ProductService productManager;

        public productOfferController(productOfferManager _productOffersManager, ProductService _productManager, OfferService _offerManager)
        {
            productOfferManager = _productOffersManager;
            productManager = _productManager;
            offerManager = _offerManager;
        }

        [HttpPost("addProductToOffer")]
        public async Task<IActionResult> addProductToOffer([FromForm] addProductToOfferViewModel _addProductToOfferViewModel)
        {

            var product = await productManager.getOne(_addProductToOfferViewModel.productId);
            var offer = await offerManager.getOne(_addProductToOfferViewModel.offerId);
            if(product == null || offer == null)
            {
                return BadRequest("Invalid product or offer ID.");
            }
            var discountValue = (product.price * offer.discount) / 100;
            var result = await productOfferManager.Add(_addProductToOfferViewModel.toModel(discountValue));
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

            var result = productOfferManager.getAll()
                .Include(p => p.product)
                .Include(p => p.offer)
                .Include(p => p.product!.category)
                .Select(po=>po.toViewModel());
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
