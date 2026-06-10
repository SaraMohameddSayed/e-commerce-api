using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DTOs;

namespace Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class deliveryController : BaseController
    {
        public GovernorateService _governorateService;
        public AreaService _areaService;
        public deliveryController(GovernorateService governorateService, AreaService areaService)
        {
            _governorateService = governorateService;
            _areaService = areaService;
        }


        //Governorate APIs
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult getGovernorates()
        {
            var governorates = _governorateService.GetAll().Include(g => g.Areas).Select(g => g.ToResponse()).ToList();
            return Ok(governorates);
        }
        [HttpGet("governorates/active")]
        public IActionResult getActiveGovernorates()
        {
            var governorates = _governorateService.GetAll().Where(g => g.IsActive == true).Include(g => g.Areas).Select(g => g.ToResponse()).ToList();
            return Ok(governorates);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("governorate")]
        public async  Task<IActionResult> addGovernorate([FromBody] AddGovernorateRequest addGovernorateRequest)
        {
            var result = await _governorateService.Add(addGovernorateRequest.ToGovernorate());
            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("governorate")]
        public async Task<IActionResult> updateGovernorate( UpdateGovernorateRequest updateGovernorateRequest)
        {


            var result = await _governorateService.UpdateGovernorate(updateGovernorateRequest);
            if (!result)
            {
                return BadRequest();
            }
            else if (result == true)
            {
                return Ok(result);

            }
            else
            {
                return BadRequest();

            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("governorateStatus/{id}")]
        public async Task<IActionResult> updateGovernorateStatus(int id)
        {
            var result = await _governorateService.UpdateGovernorateStatus(id);
            return Ok(result);
        }

        //Areas APIs

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> addArea(AddAreaRequest addAreaRequest)
        {
            var ressult = await _areaService.Add(addAreaRequest.ToArea());
            return Ok(ressult);

        }

        [Authorize(Roles = "Admin")]
        [HttpPut("area")]
        public async Task<IActionResult> updateArea(UpdateAreaRequest updateAreaRequest)
        {
            var result = await _areaService.UpdateArea(updateAreaRequest);
            return Ok(result);
        }
        [Authorize(Roles = "Admin")]
        [HttpPut("areaStatus/{id}")]
        public async Task<IActionResult> updateAreaStatus(int id)
        {
            var result = await _areaService.UpdateAreaStatus(id);
            return Ok(result);
        }
    }
}
