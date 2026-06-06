using Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Domain;
using DTOs;

namespace Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : BaseController
    {
        public CategoryManager _categoryManager;
        public CategoryController(CategoryManager categoryManager)
        {
            _categoryManager = categoryManager;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> addCategory([FromForm] AddCategoryRequest _category)
        {
            var result = await _categoryManager.Add(_category.toCategoryModel());
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

            var result =  _categoryManager.GetAll().Select(c=>c.toViewModel()).ToList();
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