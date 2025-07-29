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
    public class ProductControllerTests
    {
        private readonly Mock<IProductService> _mockService;
        private readonly ProductController _controller;

        public ProductControllerTests()
        {
            _mockService = new Mock<IProductService>();
            _controller = new ProductController(_mockService.Object);
        }

        [Fact]
        public async Task Get_ReturnsListOfProducts()
        {
            // Arrange
            _mockService.Setup(s => s.GetAllAsync())
                        .ReturnsAsync(new List<ProductDto> { new ProductDto { Id = 1, Name = "Test" } });

            // Act
            var result = await _controller.GetAllProducts();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var products = Assert.IsAssignableFrom<List<ProductDto>>(okResult.Value);
            Assert.Single(products);
        }

        [Fact]
        public async Task Get_WithInvalidId_ReturnsBadRequest()
        {
            var result = await _controller.GetProductById(0);
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task Get_WithNonExistingProduct_ReturnsNotFound()
        {
            _mockService.Setup(s => s.GetByIdAsync(99))
                                     .ReturnsAsync((ProductDto)null);
            var result = await _controller.GetProductById(99);
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Post_ReturnsCreatedProduct()
        {
            var dto = new ProductDto { Name = "New", StockQuantity = 5 };
            _mockService.Setup(s => s.CreateAsync(dto))
                                     .ReturnsAsync(new ProductDto { Id = 1, Name = "New" });

            var result = await _controller.CreateNewProduct(dto);
            var createdResult = Assert.IsType<CreatedAtActionResult>(result);
            var product = Assert.IsType<ProductDto>(createdResult.Value);
            Assert.Equal("New", product.Name);
        }
    }
}
