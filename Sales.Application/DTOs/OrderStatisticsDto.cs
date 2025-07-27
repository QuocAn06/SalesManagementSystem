using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.Application.DTOs
{
    public class OrderStatisticsDto
    {
        public DateTime Date {  get; set; }
        public int TotalOrders {  get; set; }
        public int TotalQuantity { get; set; }
        public decimal TotalRevenue { get; set; }
    }
}
