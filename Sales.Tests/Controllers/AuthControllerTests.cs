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
    public class AuthControllerTests
    {
        private readonly Mock<IUserService> _mockUserService;
        private readonly AuthController _controller;

        public AuthControllerTests()
        {
            _mockUserService = new Mock<IUserService>();
            _controller = new AuthController(_mockUserService.Object);
        }

        [Fact]
        public async Task Register_ValidInput_ReturnsOkWithUser()
        {
            var dto = new RegisterDto { Username = "test", Password = "123456" };
            var registeredUser = new UserDto { Id = 1, Username = "test" };

            _mockUserService.Setup(s => s.RegisterAsync(dto)).ReturnsAsync(registeredUser);

            var result = await _controller.Register(dto);
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedUser = Assert.IsType<UserDto>(okResult.Value);
            Assert.Equal("test", returnedUser.Username);
        }

        [Fact]
        public async Task Login_ValidCredentials_ReturnsToken()
        {
            var dto = new LoginDto { Username = "test", Password = "123456" };
            var fakeToken = "fake-jwt-token";

            _mockUserService.Setup(s => s.LoginAsync(dto)).ReturnsAsync(fakeToken);

            var result = await _controller.Login(dto);
            var okResult = Assert.IsType<OkObjectResult>(result);

            var tokenObj = okResult.Value?.GetType().GetProperty("token")?.GetValue(okResult.Value, null);
            Assert.Equal("fake-jwt-token", tokenObj);
        }

        [Fact]
        public async Task Login_InvalidCredentials_ReturnsOkWithNullToken()
        {
            var dto = new LoginDto { Username = "wrong", Password = "wrong" };

            _mockUserService.Setup(s => s.LoginAsync(dto)).ReturnsAsync((string)null);

            var result = await _controller.Login(dto);
            var okResult = Assert.IsType<OkObjectResult>(result);

            var tokenObj = okResult.Value?.GetType().GetProperty("token")?.GetValue(okResult.Value, null);
            Assert.Null(tokenObj);
        }

        [Fact]
        public async Task GetUsers_ReturnsListOfUsers()
        {
            var users = new List<UserDto>
            {
                new UserDto { Id = 1, Username = "admin" },
                new UserDto { Id = 2, Username = "user1" }
            };

            _mockUserService.Setup(s => s.GetAllAsync()).ReturnsAsync(users);

            var result = await _controller.GetUsers();
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedUsers = Assert.IsAssignableFrom<List<UserDto>>(okResult.Value);
            Assert.Equal(2, returnedUsers.Count);
        }
    }
}
