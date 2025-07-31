using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sales.Application.DTOs;
using Sales.Application.Interfaces;

namespace Sales.API.Controllers
{
    [Authorize(Roles = "Admin,Manager")]
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController: ControllerBase
    {
        private readonly IProductService _service;

        public ProductController(IProductService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _service.GetAllAsync();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid product ID.");
            }

            var product = await _service.GetByIdAsync(id);
            
            if (product == null)
            {
                return NotFound();
            }
            
            return Ok(product);
        }

        [HttpPost]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateNewProduct([FromBody] ProductDto dto)
        {
            var createdProduct = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetProductById), new { id = createdProduct.Id }, createdProduct);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] ProductDto dto)
        {
            if (id != dto.Id)
            {
                return BadRequest("Product ID mismatch.");
            }

            var updatedProduct = await _service.UpdateAsync(id, dto);
             
            if (updatedProduct == null)
            {
                return NotFound();
            }
            
            return Ok(updatedProduct);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid product ID.");
            }
            var deleted = await _service.DeleteAsync(id);
            
            if (!deleted)
            {
                return NotFound();
            }
            
            return NoContent();
        }

    }
}
