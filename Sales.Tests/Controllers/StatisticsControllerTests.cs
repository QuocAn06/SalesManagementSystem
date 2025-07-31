using Microsoft.AspNetCore.Mvc;
using Moq;
using Sales.API.Controllers;
using Sales.Application.DTOs;
using Sales.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Sales.Tests.Controllers
{
    public class StatisticsControllerTests
    {
        private readonly Mock<IStatisticsService> _mockService;
        private readonly StatisticsController _controller;

        public StatisticsControllerTests()
        {
            _mockService = new Mock<IStatisticsService>();
            _controller = new StatisticsController(_mockService.Object);
        }

        [Fact]
        public async Task GetOrderStatisticsByDateRangeAsync_ReturnsOkResult_WithValidData()
        {
            // Arrange
            var start = new DateTime(2024, 1, 1);
            var end = new DateTime(2024, 12, 31);

            var mockData = new List<OrderStatisticsDto>
                            {
                                new OrderStatisticsDto
                                {
                                    Date = new DateTime(2024, 1, 10),
                                    TotalOrders = 5,
                                    TotalQuantity = 20,
                                    TotalRevenue = 1500000
                                }
                            };

            _mockService.Setup(s => s.GetOrderStatisticsByDateRangeAsync(start, end))
                        .ReturnsAsync(mockData);

            // Act
            var result = await _controller.GetOrderStatisticsByDateRangeAsync(start, end);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnData = Assert.IsAssignableFrom<List<OrderStatisticsDto>>(okResult.Value);
            Assert.Single(returnData);
            Assert.Equal(1500000, returnData[0].TotalRevenue);
        }

        [Fact]
        public async Task GetOrderStatisticsByDateRangeAsync_ReturnsOk()
        {
            // Arrange
            var startDate = new DateTime(2024, 1, 1);
            var endDate = new DateTime(2024, 12, 31);

            var mockStats = new List<OrderStatisticsDto>
                            {
                                new OrderStatisticsDto
                                {
                                    Date = new DateTime(2024, 6, 1),
                                    TotalOrders = 5,
                                    TotalQuantity = 20,
                                    TotalRevenue = 1000000
                                }
                            };

            _mockService.Setup(s => s.GetOrderStatisticsByDateRangeAsync(startDate, endDate))
                        .ReturnsAsync(mockStats);

            // Act
            var result = await _controller.GetOrderStatisticsByDateRangeAsync(startDate, endDate);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnData = Assert.IsAssignableFrom<List<OrderStatisticsDto>>(okResult.Value);
            Assert.Single(returnData);
        }

        [Fact]
        public async Task GetOrderStatusSummaryAsync_ReturnsOkWithCorrectData()
        {
            // Arrange
            var mockSummary = new List<OrderStatusSummaryDto>
                {
                    new OrderStatusSummaryDto { Status = "Completed", TotalOrders = 10 },
                    new OrderStatusSummaryDto { Status = "Pending", TotalOrders = 5 }
                };

            _mockService.Setup(s => s.GetOrderStatusSummaryAsync())
                        .ReturnsAsync(mockSummary);

            var controller = new StatisticsController(_mockService.Object);

            // Act
            var result = await controller.GetOrderStatusSummaryAsync();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnData = Assert.IsType<List<OrderStatusSummaryDto>>(okResult.Value);

            Assert.Equal(2, returnData.Count);
            Assert.Equal("Completed", returnData[0].Status);
            Assert.Equal(10, returnData[0].TotalOrders);
        }


        [Fact]
        public async Task GetTopSellingProductsAsync_ReturnsOkWithTopProducts()
        {
            // Arrange
            int topN = 3;

            var topProducts = new List<TopProductDto>
                {
                    new TopProductDto { ProductId = 1, TotalQuantity = 50 },
                    new TopProductDto { ProductId = 2, TotalQuantity = 30 },
                };

            _mockService.Setup(s => s.GetTopSellingProductsAsync(topN))
                        .ReturnsAsync(topProducts);

            var controller = new StatisticsController(_mockService.Object);

            // Act
            var result = await controller.GetTopSellingProductsAsync(topN);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<List<TopProductDto>>(okResult.Value);
            Assert.Equal(2, returnValue.Count);
        }
    }
}
