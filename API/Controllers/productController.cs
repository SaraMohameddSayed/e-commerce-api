using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DTOs;

namespace Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : BaseController
    {
        public ProductService _productService;
        public CloudinaryService _cloudinaryService;
       

        public ProductController(ProductService productService, CloudinaryService cloudinaryService)
        {
            _productService = productService;
            _cloudinaryService = cloudinaryService;
        }
        [Authorize(Roles="Admin")]
        [HttpPost]
        public async Task<IActionResult> addProduct([FromForm] AddProductRequest addProductRequest)
        {

            var uploadImageResult = await _cloudinaryService.UploadImageAsync(addProductRequest.ImageFile);
            addProductRequest.ImageUrl = uploadImageResult;
            var result = await _productService.AddProductAsync(addProductRequest);
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
            
            var result = await _productService.GetPagedProducts(categoryId, searchText,pageNumber, pageSize);


            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromForm] UpdateProductRequest updateProductRequest)
        {
            if (id != updateProductRequest.Id)
            {
                return BadRequest("Product ID mismatch.");
            }
            if (updateProductRequest.ImageFile != null)
            {
                var uploadImageResult = await _cloudinaryService.UploadImageAsync(updateProductRequest.ImageFile);
                updateProductRequest.ImageUrl = uploadImageResult;
            }
           
            var result = await _productService.UpdateProduct(updateProductRequest);
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
            var product = await _productService.GetOne(id);
            if (product == null) return NotFound();

            product.Quantity = newStock;
            await _productService.Update(product);

            return Ok(new { message = "Stock updated successfully" });
        }
        [HttpPatch("{id}/toggle-active")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ToggleActive(int id)
        {
            var product = await _productService.GetOne(id);
            if (product == null) return NotFound();

            product.IsActive= !product.IsActive;
            await _productService.Update(product);
            return Ok(new { message = "Status updated" });
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _productService.GetOne(id);
            if (product == null)
            {
                return NotFound("Product not found.");
            }
            var result = await _productService.SoftDeleteProduct(product.Id);
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