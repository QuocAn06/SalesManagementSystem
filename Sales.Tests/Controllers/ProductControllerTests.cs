using Microsoft.AspNetCore.Mvc;
using Moq;
using Sales.API.Controllers;
using Sales.Application.DTOs;
using Sales.Application.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

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
            _mockService.Setup(s => s.GetAllAsync())
                        .ReturnsAsync(new List<ProductDto> { new ProductDto { Id = 1, Name = "Test" } });

            var result = await _controller.GetAllProducts();

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

        [Fact]
        public async Task Put_WithMismatchedId_ReturnsBadRequest()
        {
            var dto = new ProductDto { Id = 2, Name = "Updated" };
            var result = await _controller.UpdateProduct(1, dto);
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task Put_WhenNotFound_ReturnsNotFound()
        {
            var dto = new ProductDto { Id = 1, Name = "Updated" };
            _mockService.Setup(s => s.UpdateAsync(1, dto)).ReturnsAsync((ProductDto)null);

            var result = await _controller.UpdateProduct(1, dto);
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Put_WithValidProduct_ReturnsOk()
        {
            var dto = new ProductDto { Id = 1, Name = "Updated" };
            _mockService.Setup(s => s.UpdateAsync(1, dto)).ReturnsAsync(dto);

            var result = await _controller.UpdateProduct(1, dto);
            var okResult = Assert.IsType<OkObjectResult>(result);
            var updated = Assert.IsType<ProductDto>(okResult.Value);
            Assert.Equal("Updated", updated.Name);
        }

        [Fact]
        public async Task Delete_WithInvalidId_ReturnsBadRequest()
        {
            var result = await _controller.DeleteProduct(0);
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task Delete_WhenNotFound_ReturnsNotFound()
        {
            _mockService.Setup(s => s.DeleteAsync(99)).ReturnsAsync(false);

            var result = await _controller.DeleteProduct(99);
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Delete_WithValidId_ReturnsNoContent()
        {
            _mockService.Setup(s => s.DeleteAsync(1)).ReturnsAsync(true);

            var result = await _controller.DeleteProduct(1);
            Assert.IsType<NoContentResult>(result);
        }
    }
}
