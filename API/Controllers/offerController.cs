using Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Domain;
using DTOs;

namespace Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class offerController : BaseController
    {
        public OfferService offerManager;
        public ProductService productManager;
        public offerController(OfferService _offerManager,ProductService _productManager)
        {
            offerManager = _offerManager;
            productManager = _productManager;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> addOffer([FromForm] Offer _offer)
        {
            var result = await offerManager.AddOfferAsync(_offer);
            if (result)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }

        }
       


        [HttpGet]

        public IActionResult getAllOffers()
        {

            var result = offerManager.getAll().Select(offer=>offer.toViewModel());
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