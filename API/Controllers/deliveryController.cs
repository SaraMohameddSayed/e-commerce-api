using Managers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ViewModels;

namespace Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class deliveryController : ControllerBase
    {
        public governorateManager governorateManager;
        public areaManager areaManager;
        public deliveryController(governorateManager _governorateManager,areaManager _areaManager)
        {
            governorateManager = _governorateManager;
            areaManager = _areaManager;
        }


        //Governorate APIs

        [HttpGet]
        public IActionResult getGovernorates()
        {
            var governorates = governorateManager.getAll().Include(g => g.areas).Select(g => g.ToViewModel()).ToList();
            return Ok(governorates);
        }
        [HttpGet("governorates/active")]
        public IActionResult getActiveGovernorates()
        {
            var governorates = governorateManager.getAll().Where(g=>g.isActive==true).Include(g => g.areas).Select(g => g.ToViewModel()).ToList();
            return Ok(governorates);
        }
        [HttpPost("governorate")]
        public async  Task<IActionResult> addGovernorate([FromBody] addGovernorateViewModel addGovernorateViewModel)
        {
            var result = await governorateManager.Add(addGovernorateViewModel.toModel());
            return Ok(result);
        }

        [HttpPut("governorate")]
        public async Task<IActionResult> updateGovernorate( updateGovernorateViewModel updateGovernorateViewModel)
        {


            var result = await governorateManager.updateGovernorate(updateGovernorateViewModel);
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

        [HttpPut("governorateStatus/{id}")]
        public async Task<IActionResult> updateGovernorateStatus(int id)
        {
            var result = await governorateManager.updateGovernorateStatus(id);
            return Ok(result);
        }

        //Areas APIs

        [HttpPost]
        public async Task<IActionResult> addArea(addAreaViewModel addAreaViewModel)
        {
            var ressult = await areaManager.Add(addAreaViewModel.toModel());
            return Ok(ressult);

        }

        [HttpPut("area")]
        public async Task<IActionResult> updateArea(updateAreaViewModel updateAreaViewModel)
        {
            var result = await areaManager.updateArea(updateAreaViewModel);
            return Ok(result);
        }
        [HttpPut("areaStatus/{id}")]
        public async Task<IActionResult> updateAreaStatus(int id)
        {
            var result = await areaManager.updateAreaStatus(id);
            return Ok(result);
        }
    }
}
