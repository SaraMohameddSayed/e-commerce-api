using Managers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Models;
using ViewModels;

namespace Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class categoryController : BaseController
    {
        public categoryManager categoryManager;
        public categoryController(categoryManager _categoryManager)
        {
            categoryManager=_categoryManager;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> addCategory([FromForm] addCategoryViewModel _category)
        {
            var result = await categoryManager.Add(_category.toCategoryModel());
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
        public IActionResult getAllCategories()
        {

            var result =  categoryManager.getAll().Select(c=>c.toViewModel()).ToList();
            if (result!=null)
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