using Microsoft.AspNetCore.Mvc;
using Sales.Application.Interfaces;
using Sales.Infrastructure.Services;

namespace Sales.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StatisticsController : ControllerBase
    {
        private readonly IStatisticsService _service;

        public StatisticsController(IStatisticsService service)
        {
            _service = service;
        }

        [HttpGet("orders")]
        public async Task<IActionResult> GetOrderStatisticsByDateRangeAsync([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            var result = await _service.GetOrderStatisticsByDateRangeAsync(startDate, endDate);
            return Ok(result);
        }

        [HttpGet("orders/monthly-revenue")]
        public async Task<IActionResult> GetMonthlyRevenueStatisticsAsync([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            var result = await _service.GetMonthlyRevenueStatisticsAsync(startDate, endDate);
            return Ok(result);
        }

        [HttpGet("orders/status-summary")]
        public async Task<IActionResult> GetOrderStatusSummaryAsync()
        {
            var result = await _service.GetOrderStatusSummaryAsync();
            return Ok(result);
        }

        [HttpGet("top_products")]
        public async Task<IActionResult> GetTopSellingProductsAsync([FromQuery] int topN)
        {
            var result = await _service.GetTopSellingProductsAsync(topN);
            return Ok(result);
        }

    }
}
