using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.Application.DTOs
{
    public class OrderStatusSummaryDto
    {
        public required string Status { get; set; }
        public int TotalOrders { get; set; }
    }
}
