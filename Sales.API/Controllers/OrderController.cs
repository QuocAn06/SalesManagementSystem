using Microsoft.AspNetCore.Mvc;
using Sales.Application.DTOs;
using Sales.Application.Interfaces;
using Sales.Infrastructure.Services;

namespace Sales.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _service;

        public OrderController(IOrderService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var orders = await _service.GetAllAsync();
            return Ok(orders);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid order ID.");

            var order = await _service.GetByIdAsync(id);

            if (order == null)
                return NotFound();

            return Ok(order);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] OrderDto orderDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var created = await _service.CreateAsync(orderDto);
            return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
        }

        // PUT: api/order/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] OrderDto orderDto)
        {
            if (id != orderDto.Id)
                return BadRequest("Order ID mismatch.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updated = await _service.UpdateAsync(id, orderDto);

            if (updated == null)
                return NotFound();

            return Ok(updated);
        }

        // DELETE: api/order/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _service.DeleteAsync(id);

            if (!deleted)
                return NotFound();

            return NoContent();
        }

    }
}
