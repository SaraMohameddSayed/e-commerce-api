using Application.Services;
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
        public CategoryService _categoryService;
        public CategoryController(CategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> addCategory([FromForm] AddCategoryRequest _category)
        {
            var result = await _categoryService.Add(_category.ToCategory());
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

            var result =  _categoryService.GetAll().Select(c=>c.ToResponse()).ToList();
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