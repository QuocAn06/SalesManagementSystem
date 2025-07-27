using Sales.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.Application.Interfaces
{
    public interface IOrderService
    {
        Task<OrderDto> CreateAsync(OrderDto orderDto);
        Task<List<OrderDto>> GetAllAsync();
        Task<OrderDto> GetByIdAsync(int id);
        Task<OrderDto> UpdateAsync(int orderId, OrderDto orderDto);
        Task<bool> DeleteAsync(int orderId);
    }
}
