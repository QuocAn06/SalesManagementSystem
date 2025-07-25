using Microsoft.AspNetCore.Mvc;
using Moq;
using Sales.API.Controllers;
using Sales.Application.DTOs;
using Sales.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.Tests.Controllers
{
    public class OrderControllerTests
    {
        private readonly Mock<IOrderService> _mockService;
        private readonly OrderController _controller;

        public OrderControllerTests()
        {
            _mockService = new Mock<IOrderService>();
            _controller = new OrderController(_mockService.Object);
        }

        [Fact]
        public async Task Get_ReturnsListOfOrders()
        {
            _mockService.Setup(s => s.GetAllAsync())
                        .ReturnsAsync(new List<OrderDto> { new OrderDto { Id = 1,
                                                                          TotalAmount = 100,
                                                                          OrderDetails = [new OrderDetailDto 
                                                                            { ProductId = 1,
                                                                              Quantity = 2,
                                                                              UnitPrice = 34000}]}});

            var result = await _controller.Get();

            var okResult = Assert.IsType<OkObjectResult>(result);
            var orders = Assert.IsAssignableFrom<List<OrderDto>>(okResult.Value);
            Assert.Single(orders);
        }

        [Fact]
        public async Task Get_WithInvalidId_ReturnsBadRequest()
        {
            var result = await _controller.Get(0);
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task Get_WithNonExistingOrder_ReturnsNotFound()
        {
            _mockService.Setup(s => s.GetByIdAsync(99)).ReturnsAsync((OrderDto)null);
            var result = await _controller.Get(99);
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Post_ReturnsCreatedOrder()
        {
            var dto = new OrderDto
            {
                CustomerId = 1,
                UserId = 1,
                OrderDetails = new List<OrderDetailDto> {
                new OrderDetailDto { ProductId = 1, Quantity = 2, UnitPrice = 50 }
            }
            };
            _mockService.Setup(s => s.CreateAsync(dto))
                        .ReturnsAsync(new OrderDto { Id = 1,
                                                     TotalAmount = 100,
                                                     OrderDetails = [new OrderDetailDto 
                                                                         { ProductId = 1,
                                                                           Quantity = 2,
                                                                           UnitPrice = 34000}]});

            var result = await _controller.Post(dto);
            var created = Assert.IsType<CreatedAtActionResult>(result);
            var order = Assert.IsType<OrderDto>(created.Value);
            Assert.Equal(1, order.Id);
        }

        [Fact]
        public async Task Put_WithIdMismatch_ReturnsBadRequest()
        {
            var dto = new OrderDto { Id = 2, OrderDetails = [new OrderDetailDto
                                                                         { ProductId = 1,
                                                                           Quantity = 2,
                                                                           UnitPrice = 34000}]};
            var result = await _controller.Put(1, dto);
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task Put_WithValidData_ReturnsUpdatedOrder()
        {
            var dto = new OrderDto { Id = 1, CustomerId = 1, UserId = 1, OrderDetails = new List<OrderDetailDto>() };
            _mockService.Setup(s => s.UpdateAsync(1, dto))
                        .ReturnsAsync(dto);

            var result = await _controller.Put(1, dto);
            var ok = Assert.IsType<OkObjectResult>(result);
            var updated = Assert.IsType<OrderDto>(ok.Value);
            Assert.Equal(1, updated.Id);
        }

        [Fact]
        public async Task Delete_WithNonExistingOrder_ReturnsNotFound()
        {
            _mockService.Setup(s => s.DeleteAsync(99)).ReturnsAsync(false);
            var result = await _controller.Delete(99);
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Delete_WithExistingOrder_ReturnsNoContent()
        {
            _mockService.Setup(s => s.DeleteAsync(1)).ReturnsAsync(true);
            var result = await _controller.Delete(1);
            Assert.IsType<NoContentResult>(result);
        }
    }
}
