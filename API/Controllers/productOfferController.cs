using Managers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ViewModels;

namespace El_beqala_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class productOfferController : ControllerBase
    {

        public productOfferManager productOfferManager;
        public offerManager offerManager;
        public productManager productManager;

        public productOfferController(productOfferManager _productOffersManager, productManager _productManager, offerManager _offerManager)
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
            var result = await productOfferManager.addProductToOffer(_addProductToOfferViewModel.toModel(discountValue));
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
