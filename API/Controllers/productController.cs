using Managers;
using Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class productController : ControllerBase
    {
        public productManager productManager;
        public cloudinaryManager cloudinaryManager;

        public productController(productManager _productManager,cloudinaryManager _cloudinaryManager)
        {
            productManager=_productManager;
            cloudinaryManager = _cloudinaryManager;
        }

        [HttpPost]
        public async Task<IActionResult> addProduct([FromForm]addProductViewModel _addProductViewModel) {

            var uploadImageResult=await cloudinaryManager.UploadImageAsync(_addProductViewModel.imageFile);
            _addProductViewModel.imageUrl = uploadImageResult;
            var result = await productManager.Add(_addProductViewModel.toModel());
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
        public IActionResult GetAllProducts(int pageNumber = 1, int pageSize = 10)
        {
            var (items, totalCount) = productManager.GetPagedProducts(pageNumber, pageSize);

            var response = new
            {
                items,
                totalCount,
                pageNumber,
                pageSize,
                totalPages = (int)Math.Ceiling(totalCount / (double)pageSize)
            };

            return Ok(response);
        }
    }
}
