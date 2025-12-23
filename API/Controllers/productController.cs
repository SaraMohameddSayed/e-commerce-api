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

        public productController(productManager _productManager, cloudinaryManager _cloudinaryManager)
        {
            productManager = _productManager;
            cloudinaryManager = _cloudinaryManager;
        }

        [HttpPost]
        public async Task<IActionResult> addProduct([FromForm] addProductViewModel _addProductViewModel)
        {

            var uploadImageResult = await cloudinaryManager.UploadImageAsync(_addProductViewModel.imageFile);
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
        public async Task<IActionResult> GetAllProducts(int? categoryId, string? searchText,int pageNumber = 1, int pageSize = 9)
        {
            pageNumber=pageNumber<1?1:pageNumber;
            pageSize=pageSize>9?9:pageSize;
            var result = await productManager.GetPagedProducts(categoryId, searchText,pageNumber, pageSize);


            return Ok(result);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromForm] updateProductViewModel _updateProductViewModel)
        {
            if (id != _updateProductViewModel.id)
            {
                return BadRequest("Product ID mismatch.");
            }
            if (_updateProductViewModel.imageFile != null)
            {
                var uploadImageResult = await cloudinaryManager.UploadImageAsync(_updateProductViewModel.imageFile);
                _updateProductViewModel.imageUrl = uploadImageResult;
            }
           
            var result = await productManager.updateProduct(_updateProductViewModel);
            if (result)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await productManager.getOne(id);
            if (product == null)
            {
                return NotFound("Product not found.");
            }
            var result = await productManager.Delete(product);
            if (result)
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