using Sales.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.Application.Interfaces
{
    public interface IStatisticsService
    {
        Task<List<OrderStatisticsDto>> GetOrderStatisticsByDateRangeAsync(DateTime startDate, DateTime endDate);
        Task<List<OrderStatisticsDto>> GetMonthlyRevenueStatisticsAsync(DateTime startDate, DateTime endDate);
        Task<List<TopProductDto>> GetTopSellingProductsAsync(int topN);
        Task<List<OrderStatusSummaryDto>> GetOrderStatusSummaryAsync();
    }
}
