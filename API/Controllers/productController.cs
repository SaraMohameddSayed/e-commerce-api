using Infrastructure;
using Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Domain;
using DTOs;

namespace Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class productController : BaseController
    {
        public ProductService productManager;
        public CloudinaryService cloudinaryManager;
       

        public productController(ProductService _productManager, CloudinaryService _cloudinaryManager)
        {
            productManager = _productManager;
            cloudinaryManager = _cloudinaryManager;
        }
        [Authorize(Roles="Admin")]
        [HttpPost]
        public async Task<IActionResult> addProduct([FromForm] addProductViewModel _addProductViewModel)
        {

            var uploadImageResult = await cloudinaryManager.UploadImageAsync(_addProductViewModel.imageFile);
            _addProductViewModel.imageUrl = uploadImageResult;
            var result = await productManager.addProductAsync(_addProductViewModel);
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
            
            var result = await productManager.GetPagedProducts(categoryId, searchText,pageNumber, pageSize);


            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
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
        [HttpPatch("{id}/stock")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateStock(int id, [FromBody]int newStock)
        {
            var product = await productManager.getOne(id);
            if (product == null) return NotFound();

            product.quantity = newStock;
            await productManager.Update(product);

            return Ok(new { message = "Stock updated successfully" });
        }
        [HttpPatch("{id}/toggle-active")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ToggleActive(int id)
        {
            var product = await productManager.getOne(id);
            if (product == null) return NotFound();

            product.isActive = !product.isActive;
            await productManager.Update(product);

            return Ok(new { message = "Status updated" });
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await productManager.getOne(id);
            if (product == null)
            {
                return NotFound("Product not found.");
            }
            var result = await productManager.softDeleteProduct(product.id);
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