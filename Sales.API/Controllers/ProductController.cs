using Microsoft.AspNetCore.Mvc;
using Sales.Application.Interfaces;
using Sales.Application.DTOs;

namespace Sales.API.Controllers
{
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
        public async Task<IActionResult> Get()
        {
            var products = await _service.GetAllProductAsync();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid product ID.");
            }

            var product = await _service.GetProductByIdAsync(id);
            
            if (product == null)
            {
                return NotFound();
            }
            
            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ProductDto dto)
        {
            var createdProduct = await _service.CreateProductAsync(dto);
            return CreatedAtAction(nameof(Get), new { id = createdProduct.Id }, createdProduct);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] ProductDto dto)
        {
            if (id != dto.Id)
            {
                return BadRequest("Product ID mismatch.");
            }

            var updatedProduct = await _service.UpdateProductAsync(id, dto);
             
            if (updatedProduct == null)
            {
                return NotFound();
            }
            
            return Ok(updatedProduct);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid product ID.");
            }
            var deleted = await _service.DeleteProductAsync(id);
            
            if (!deleted)
            {
                return NotFound();
            }
            
            return NoContent();
        }

    }
}
