using Managers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using ViewModels;

namespace Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class offerController : ControllerBase
    {
        public offerManager offerManager;
        public productManager productManager;
        public offerController(offerManager _offerManager,productManager _productManager)
        {
            offerManager = _offerManager;
            productManager = _productManager;
        }

        [HttpPost]
        public async Task<IActionResult> addOffer([FromForm] Offer _offer)
        {
            var result = await offerManager.Add(_offer);
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