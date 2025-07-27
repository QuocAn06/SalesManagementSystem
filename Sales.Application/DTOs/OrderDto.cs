using Sales.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.Application.DTOs
{
    public class OrderDto
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int UserId { get; set; }
        public required List<OrderDetailDto> OrderDetails { get; set; }

        public decimal TotalAmount { get; set; }
        public OrderStatus Status { get; set; }

    }
}
