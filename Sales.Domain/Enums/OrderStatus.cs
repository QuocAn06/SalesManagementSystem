using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.Domain.Enums
{
    public enum OrderStatus
    {
        Pending = 0,        // Chờ xử lý
        Confirmed = 1,      // Đã xác nhận
        Shipped = 2,        // Đã giao hàng
        Completed = 3,      // Hoàn thành
        Cancelled = 4       // Đã hủy
    }
}
