using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Sales.Application.DTOs;
using Sales.Application.Interfaces;
using Sales.Domain.Entities;
using Sales.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.Infrastructure.Services
{
    public class OrderService: IOrderService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public OrderService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<OrderDto> CreateAsync(OrderDto orderDto)
        {
            var order = _mapper.Map<Order>(orderDto);

            // Tính TotalPrice cho từng chi tiết
            foreach (var detail in order.OrderDetails)
            {
                detail.TotalPrice = detail.Quantity * detail.UnitPrice;
            }

            // Tính tổng đơn hàng
            order.TotalAmount = order.OrderDetails.Sum(d => d.TotalPrice);
            order.OrderDate = DateTime.Now;

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            var createdDto = _mapper.Map<OrderDto>(order);
            return createdDto;
        }

        public async Task<List<OrderDto>> GetAllAsync() {
            var orders = await _context.Orders.ToListAsync();
            var orderDtos = _mapper.Map<List<OrderDto>>(orders);
            return orderDtos;
        }

        public async Task<OrderDto> GetByIdAsync(int id)
        {
            var order = await _context.Orders.FindAsync(id);

            if(order == null)
            {
                return null;
            }

            var orderDto = _mapper.Map<OrderDto>(order);
            return orderDto;
        }

        public async Task<OrderDto> UpdateAsync(int id, OrderDto dto)
        {
            var order = await _context.Orders
                .Include(o => o.OrderDetails)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (order == null)
                return null;

            // Cập nhật thông tin đơn hàng
            order.CustomerId = dto.CustomerId;
            order.UserId = dto.UserId;
            order.OrderDate = DateTime.Now;
            order.Status = dto.Status;

            // Cập nhật lại danh sách chi tiết đơn hàng
            order.OrderDetails.Clear();

            foreach (var detailDto in dto.OrderDetails)
            {
                order.OrderDetails.Add(new OrderDetail
                {
                    ProductId = detailDto.ProductId,
                    Quantity = detailDto.Quantity,
                    UnitPrice = detailDto.UnitPrice,
                    TotalPrice = detailDto.UnitPrice * detailDto.Quantity
                });
            }

            order.TotalAmount = order.OrderDetails.Sum(d => d.TotalPrice);

            await _context.SaveChangesAsync();

            return _mapper.Map<OrderDto>(order);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var order = await _context.Orders
                .Include(o => o.OrderDetails)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (order == null)
                return false;

            _context.OrderDetails.RemoveRange(order.OrderDetails);
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
