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
    public class CustomerControllerTests
    {
        private readonly Mock<ICustomerService> _mockService;
        private readonly CustomerController _controller;

        public CustomerControllerTests()
        {
            _mockService = new Mock<ICustomerService>();
            _controller = new CustomerController(_mockService.Object);
        }

        [Fact]
        public async Task Get_ReturnsListOfCustomers()
        {
            _mockService.Setup(s => s.GetAllAsync())
                        .ReturnsAsync(new List<CustomerDto> { new CustomerDto { Id = 1, Name = "An Le" } });

            var result = await _controller.Get();

            var okResult = Assert.IsType<OkObjectResult>(result);
            var customers = Assert.IsAssignableFrom<List<CustomerDto>>(okResult.Value);
            Assert.Single(customers);
        }

        [Fact]
        public async Task Get_WithInvalidId_ReturnBadRequest()
        {
            var result = await _controller.Get(0);
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task Get_WithNonExistingCustomer_ReturnsNotFound()
        {
            _mockService.Setup(s => s.GetByIdAsync(99))
                        .ReturnsAsync((CustomerDto)null);
            var result = await _controller.Get(99);
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Post_ReturnsCreatedCustomer()
        {
            var dto = new CustomerDto { Name = "Minh Truong" };
            _mockService.Setup(s => s.CreateAsync(dto))
                        .ReturnsAsync(new CustomerDto { Id = 10, Name = "Minh Truong" });
            
            var result = await _controller.Post(dto);
            var createdResult = Assert.IsType<CreatedAtActionResult>(result);
            var customer = Assert.IsType<CustomerDto>(createdResult.Value);
            Assert.Equal("Minh Truong", customer.Name);
        }
    }
}
