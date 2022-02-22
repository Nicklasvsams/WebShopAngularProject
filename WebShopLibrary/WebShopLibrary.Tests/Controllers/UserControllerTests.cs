using Microsoft.AspNetCore.Mvc.Infrastructure;
using Moq;
using System;
using System.Collections.Generic;
using WebShopLibrary.BusinessAccessLayer.DTOs.UserDTO;
using WebShopLibrary.BusinessAccessLayer.Services.UserServices;
using WebShopLibrary.WebApi.Controllers.UserControllers;
using Xunit;

namespace WebShopLibrary.Tests.Controllers
{
    public class UserControllerTests
    {
        private readonly UserController _usercontroller;
        private readonly Mock<IUserService> _mockUserService = new Mock<IUserService>();

        public UserControllerTests()
        {
            _usercontroller = new UserController(_mockUserService.Object);
        }

        [Fact]
        public async void GetAll_ShouldReturnStatusCode204_WhenNoUsersExist()
        {
            // Arrange
            List<UserResponse> users = new List<UserResponse>();

            _mockUserService
                .Setup(x => x.GetAllUsers())
                .ReturnsAsync(users);

            // Act
            var result = await _usercontroller.GetAll();
            var statusCodeResult = (IStatusCodeActionResult)result;

            // Assert
            Assert.Equal(204, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void GetAll_ShouldReturnStatusCode200_WhenUsersExist()
        {
            // Arrange
            List<UserResponse> users = new List<UserResponse>();

            users.AddRange(UserResponseList());

            _mockUserService
                .Setup(x => x.GetAllUsers())
                .ReturnsAsync(users);

            // Act
            var result = await _usercontroller.GetAll();
            var statusCodeResult = (IStatusCodeActionResult)result;

            // Assert
            Assert.Equal(200, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void GetAll_ShouldReturnStatusCode500_WhenServiceReturnsNull()
        {
            // Arrange
            _mockUserService
                .Setup(x => x.GetAllUsers())
                .ReturnsAsync(() => null);

            // Act
            var result = await _usercontroller.GetAll();
            var statusCodeResult = (IStatusCodeActionResult)result;

            // Assert
            Assert.Equal(500, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void GetAll_ShouldReturnStatusCode500_WhenExceptionIsRaised()
        {
            // Arrange
            _mockUserService
                .Setup(x => x.GetAllUsers())
                .ReturnsAsync(() => throw new Exception("test"));

            // Act
            var result = await _usercontroller.GetAll();
            var statusCodeResult = (IStatusCodeActionResult)result;

            // Assert
            Assert.Equal(500, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void GetById_ShouldReturnStatusCode200_WhenUserExists()
        {
            // Arrange
            int purchaseId = 1;

            _mockUserService
                .Setup(x => x.GetUserById(It.IsAny<int>()))
                .ReturnsAsync(UserResponse());

            // Act
            var result = await _usercontroller.GetById(purchaseId);
            var statusCodeResult = (IStatusCodeActionResult)result;

            // Assert
            Assert.Equal(200, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void GetById_ShouldReturnStatusCode500_WhenExceptionIsRaised()
        {
            // Arrange
            int userId = 1;

            _mockUserService
                .Setup(x => x.GetUserById(It.IsAny<int>()))
                .ReturnsAsync(() => throw new Exception("Test"));

            // Act
            var result = await _usercontroller.GetById(userId);
            var statusCodeResult = (IStatusCodeActionResult)result;

            // Assert
            Assert.Equal(500, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void GetById_ShouldReturnStatusCode404_WhenUserDoesNotExist()
        {
            // Arrange
            int userId = 1;

            _mockUserService
                .Setup(x => x.GetUserById(It.IsAny<int>()))
                .ReturnsAsync(() => null);

            // Act
            var result = await _usercontroller.GetById(userId);
            var statusCodeResult = (IStatusCodeActionResult)result;

            // Assert
            Assert.Equal(404, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void Create_ShouldReturnStatusCode200_WhenUserIsCreated()
        {
            // Arrange
            _mockUserService
                .Setup(x => x.CreateUser(It.IsAny<UserRequest>()))
                .ReturnsAsync(UserResponse());

            // Act
            var result = await _usercontroller.Create(UserRequest());
            var statusCodeResult = (IStatusCodeActionResult)result;

            // Assert
            Assert.Equal(200, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void Create_ShouldReturnStatusCode500_WhenExceptionIsRaised()
        {
            // Arrange
            _mockUserService
                .Setup(x => x.CreateUser(It.IsAny<UserRequest>()))
                .ReturnsAsync(() => throw new Exception("Test"));

            // Act
            var result = await _usercontroller.Create(UserRequest());
            var statusCodeResult = (IStatusCodeActionResult)result;

            // Assert
            Assert.Equal(500, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void Update_ShouldReturnStatusCode200_WhenUpdateIsSuccessful()
        {
            // Arrange
            int userId = 1;

            _mockUserService
                .Setup(x => x.UpdateUser(It.IsAny<int>(), It.IsAny<UserRequest>()))
                .ReturnsAsync(UserResponse());

            // Act
            var result = await _usercontroller.Update(userId, UserRequest());
            var statusCodeResult = (IStatusCodeActionResult)result;

            // Assert
            Assert.Equal(200, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void Update_ShouldReturnStatusCode404_WhenUserToUpdateIsNotFound()
        {
            // Arrange
            int userId = 1;

            _mockUserService
                .Setup(x => x.UpdateUser(It.IsAny<int>(), It.IsAny<UserRequest>()))
                .ReturnsAsync(() => null);

            // Act
            var result = await _usercontroller.Update(userId, UserRequest());
            var statusCodeResult = (IStatusCodeActionResult)result;

            // Assert
            Assert.Equal(404, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void Update_ShouldReturnStatusCode500_WhenExceptionIsRaised()
        {
            // Arrange
            int userId = 1;

            _mockUserService
                .Setup(x => x.UpdateUser(It.IsAny<int>(), It.IsAny<UserRequest>()))
                .ReturnsAsync(() => throw new Exception("Test"));

            // Act
            var result = await _usercontroller.Update(userId, UserRequest());
            var statusCodeResult = (IStatusCodeActionResult)result;

            // Assert
            Assert.Equal(500, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void Delete_ShouldReturnStatusCode200_WhenDeleteIsSuccessful()
        {
            // Arrange
            int userId = 1;

            _mockUserService
                .Setup(x => x.DeleteUser(It.IsAny<int>()))
                .ReturnsAsync(UserResponse());

            // Act
            var result = await _usercontroller.Delete(userId);
            var statusCodeResult = (IStatusCodeActionResult)result;

            // Assert
            Assert.Equal(200, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void Delete_ShouldReturnStatusCode404_WhenUserToDeleteIsNotFound()
        {
            // Arrange
            int userId = 1;

            _mockUserService
                .Setup(x => x.DeleteUser(It.IsAny<int>()))
                .ReturnsAsync(() => null);

            // Act
            var result = await _usercontroller.Delete(userId);
            var statusCodeResult = (IStatusCodeActionResult)result;

            // Assert
            Assert.Equal(404, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void Delete_ShouldReturnStatusCode500_WhenExceptionIsRaised()
        {
            // Arrange
            int userId = 1;

            _mockUserService
                .Setup(x => x.DeleteUser(It.IsAny<int>()))
                .ReturnsAsync(() => throw new Exception("Test"));

            // Act
            var result = await _usercontroller.Delete(userId);
            var statusCodeResult = (IStatusCodeActionResult)result;

            // Assert
            Assert.Equal(500, statusCodeResult.StatusCode);
        }

        private UserResponse UserResponse()
        {
            return new UserResponse()
            {
                Id = 1,
                Username = "UserUsernameTest",
                Password = "UserPasswordTest",
                Email = "UserEmailTest@Test.dk",
                UserType = "UserUserTypeTest"
            };
        }

        private UserRequest UserRequest()
        {
            return new UserRequest()
            {
                Username = "UserUsernameTest",
                Password = "UserPasswordTest",
                Email = "UserEmailTest@Test.dk",
                UserType = "UserUserTypeTest"
            };
        }

        private List<UserResponse> UserResponseList()
        {
            return new List<UserResponse>()
            {
                new UserResponse()
                {
                    Id = 1,
                    Username = "UserUsernameTest",
                    Password = "UserPasswordTest",
                    Email = "UserEmailTest@Test.dk",
                    UserType = "UserUserTypeTest"
                },
                new UserResponse()
                {
                    Id = 1,
                    Username = "UserUsernameTest2",
                    Password = "UserPasswordTest2",
                    Email = "UserEmailTest2@Test.dk",
                    UserType = "UserUserTypeTest2"
                }
            };
        }
    }
}
