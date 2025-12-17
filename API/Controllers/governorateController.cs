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
    public class governorateController : ControllerBase
    {
        public governorateManager governorateManager;
        public governorateController(governorateManager _governorateManager) { 
            governorateManager = _governorateManager;
        }
        [HttpGet]
        public IActionResult getGovernorates()
        {
            var governorates = governorateManager.getAll().Include(g => g.areas).Select(g => g.ToViewModel()).ToList();
            return Ok(governorates);
        }
    }
}
