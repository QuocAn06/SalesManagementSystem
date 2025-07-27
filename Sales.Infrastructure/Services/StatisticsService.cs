using Microsoft.EntityFrameworkCore;
using Sales.Application.DTOs;
using Sales.Application.Interfaces;
using Sales.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.Infrastructure.Services
{
    public class StatisticsService : IStatisticsService
    {
        private readonly ApplicationDbContext _context;

        public StatisticsService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<OrderStatisticsDto>> GetMonthlyRevenueStatisticsAsync(DateTime startDate, DateTime endDate)
        {
            var query = await _context.Orders.Where(o => o.OrderDate >= startDate && o.OrderDate <= endDate)
                                             .Include(o=>o.OrderDetails)
                                             .GroupBy(o=> new {o.OrderDate.Year, o.OrderDate.Month})
                                             .Select(g => new OrderStatisticsDto
                                             {
                                                 Date = new DateTime(g.Key.Year, g.Key.Month, 1),
                                                 TotalOrders = g.Count(),
                                                 TotalQuantity = g.Sum(x => x.OrderDetails.Sum(od => od.Quantity)),
                                                 TotalRevenue = g.Sum(x => x.OrderDetails.Sum(od => od.TotalPrice))
                                             }).ToListAsync();

            return query;
        }

        public async Task<List<OrderStatisticsDto>> GetOrderStatisticsByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            var query = await _context.Orders.Where(o => o.OrderDate >= startDate && o.OrderDate <= endDate)
                                             .Include(o => o.OrderDetails)
                                             .GroupBy(o => o.OrderDate.Date)
                                             .Select(g => new OrderStatisticsDto
                                             {
                                                 Date = g.Key,
                                                 TotalOrders = g.Count(),
                                                 TotalQuantity = g.Sum(x => x.OrderDetails.Sum(od => od.Quantity)),
                                                 TotalRevenue = g.Sum(x => x.OrderDetails.Sum(od => od.TotalPrice))
                                             }).ToListAsync();

            return query;
        }

        public async Task<List<OrderStatusSummaryDto>> GetOrderStatusSummaryAsync()
        {
            var query = await _context.Orders.GroupBy(o => o.Status)
                                             .Select(g => new OrderStatusSummaryDto
                                             {
                                                 Status = g.Key.ToString(),
                                                 TotalOrders = g.Count()
                                             }).ToListAsync();

            return query;
        }

        public async Task<List<TopProductDto>> GetTopSellingProductsAsync(int topN)
        {
            var query = await _context.OrderDetails.GroupBy(od => od.ProductId)
                                                   .Select(g => new TopProductDto
                                                   {
                                                       ProductId = g.Key,
                                                       TotalQuantity = g.Sum(od => od.Quantity)
                                                   })
                                                   .OrderByDescending(x => x.TotalQuantity)
                                                   .Take(topN).ToListAsync();

            return query;
        }
    }
}
