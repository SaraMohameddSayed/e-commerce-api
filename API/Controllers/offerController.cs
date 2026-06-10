using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Domain;
using DTOs;

namespace Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OfferController : BaseController
    {
        public OfferService _offerService;
        public ProductService _productService;
        public OfferController(OfferService offerService, ProductService productService)
        {
            _offerService = offerService;
            _productService = productService;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> addOffer([FromForm] Offer _offer)
        {
            var result = await _offerService.AddOfferAsync(_offer);
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

            var result = _offerService.GetAll().Select(offer=>offer.ToResponse());
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