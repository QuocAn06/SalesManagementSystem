using Microsoft.AspNetCore.Mvc;
using Sales.Application.DTOs;
using Sales.Application.Interfaces;

namespace Sales.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController: ControllerBase
    {
        private readonly ICustomerService _service;

        public CustomerController(ICustomerService service) { 
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var customers = await _service.GetAllAsync();
            return Ok(customers);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid customer ID.");
            }
            
            var customer = await _service.GetByIdAsync(id);

            if (customer == null)
            {
                return NotFound();
            }

            return Ok(customer);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CustomerDto dto)
        {
            var newCustomer = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(Get), new { id = newCustomer.Id }, newCustomer);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] CustomerDto dto)
        {
            if (id != dto.Id)
            {
                return BadRequest("Customer ID mismatch.");
            }

            var updatedCustomer = await _service.UpdateAsync(id, dto);

            if (updatedCustomer == null)
            {
                return NotFound();
            }

            return Ok(updatedCustomer);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid customer ID.");
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
