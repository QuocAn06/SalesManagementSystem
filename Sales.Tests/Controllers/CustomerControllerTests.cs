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
using Xunit;

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
        public async Task GetReturnsListOfCustomers()
        {
            _mockService.Setup(s => s.GetAllAsync())
                        .ReturnsAsync(new List<CustomerDto> { new CustomerDto { Id = 1, Name = "An Le" } });

            var result = await _controller.GetAllCustomers();

            var okResult = Assert.IsType<OkObjectResult>(result);
            var customers = Assert.IsAssignableFrom<List<CustomerDto>>(okResult.Value);
            Assert.Single(customers);
        }

        [Fact]
        public async Task Get_WithInvalidId_ReturnBadRequest()
        {
            var result = await _controller.GetCustomerById(0);
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task Get_WithNonExistingCustomer_ReturnsNotFound()
        {
            _mockService.Setup(static s => s.GetByIdAsync(99))
                         .ReturnsAsync((CustomerDto)null);
            var result = await _controller.GetCustomerById(99);
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Post_ReturnsCreatedCustomer()
        {
            var dto = new CustomerDto { Name = "Minh Truong" };
            _mockService.Setup(s => s.CreateAsync(dto))
                        .ReturnsAsync(new CustomerDto { Id = 10, Name = "Minh Truong" });

            var result = await _controller.CreateNewCustomer(dto);
            var createdResult = Assert.IsType<CreatedAtActionResult>(result);
            var customer = Assert.IsType<CustomerDto>(createdResult.Value);
            Assert.Equal("Minh Truong", customer.Name);
        }

        [Fact]
        public async Task UpdateCustomer_IdMismatch_ReturnsBadRequest()
        {
            var dto = new CustomerDto { Id = 2, Name = "Mismatch" };
            var result = await _controller.UpdateCustomer(1, dto);
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task UpdateCustomer_NotFound_ReturnsNotFound()
        {
            var dto = new CustomerDto { Id = 5, Name = "Khong Co" };
            _mockService.Setup(s => s.UpdateAsync(5, dto)).ReturnsAsync((CustomerDto)null);

            var result = await _controller.UpdateCustomer(5, dto);
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task UpdateCustomer_Valid_ReturnsOk()
        {
            var dto = new CustomerDto { Id = 3, Name = "Updated Name" };
            _mockService.Setup(s => s.UpdateAsync(3, dto)).ReturnsAsync(dto);

            var result = await _controller.UpdateCustomer(3, dto);
            var okResult = Assert.IsType<OkObjectResult>(result);
            var customer = Assert.IsType<CustomerDto>(okResult.Value);
            Assert.Equal("Updated Name", customer.Name);
        }

        [Fact]
        public async Task DeleteCustomer_Valid_ReturnsNoContent()
        {
            _mockService.Setup(s => s.DeleteAsync(1)).ReturnsAsync(true);

            var result = await _controller.DeleteCustomer(1);
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task DeleteCustomer_InvalidId_ReturnsBadRequest()
        {
            var result = await _controller.DeleteCustomer(0);
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task DeleteCustomer_NotFound_ReturnsNotFound()
        {
            _mockService.Setup(s => s.DeleteAsync(99)).ReturnsAsync(false);

            var result = await _controller.DeleteCustomer(99);
            Assert.IsType<NotFoundResult>(result);
        }
    }
}
