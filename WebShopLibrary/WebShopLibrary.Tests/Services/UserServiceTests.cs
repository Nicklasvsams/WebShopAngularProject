using Moq;
using System.Collections.Generic;
using WebShopLibrary.BusinessAccessLayer.DTOs.UserDTO;
using WebShopLibrary.BusinessAccessLayer.Services.UserServices;
using WebShopLibrary.DataAccessLayer.Database.Entities.Users;
using WebShopLibrary.DataAccessLayer.Repositories.UserRepositories;
using Xunit;

namespace WebShopLibrary.Tests.Services
{
    public class UserServiceTests
    {
        private readonly IUserService _userService;
        private readonly Mock<IUserRepository> _mockUserRepository = new Mock<IUserRepository>();

        public UserServiceTests()
        {
            _userService = new UserService(_mockUserRepository.Object);
        }

        [Fact]
        public async void GetAllUsers_ShouldReturnListOfUserResponses_WhenUsersExist()
        {
            // Arrange
            _mockUserRepository
                .Setup(x => x.SelectAllUsers())
                .ReturnsAsync(UserList());

            // Act
            var result = await _userService.GetAllUsers();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<UserResponse>>(result);
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async void GetAllUsers_ShouldReturnEmptyListOfUserResponses_WhenNoUsersExist()
        {
            // Arrange
            List<User> users = new List<User>();

            _mockUserRepository
                .Setup(x => x.SelectAllUsers())
                .ReturnsAsync(users);

            // Act
            var result = await _userService.GetAllUsers();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<UserResponse>>(result);
            Assert.Empty(result);
        }

        [Fact]
        public async void GetUserById_ShouldReturnSingleUserResponse_WhenUserExists()
        {
            // Arrange
            int userId = 1;

            _mockUserRepository
                .Setup(x => x.SelectUserById(It.IsAny<int>()))
                .ReturnsAsync(User());

            // Act
            var result = await _userService.GetUserById(userId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<UserResponse>(result);
            Assert.Equal(userId, result.Id);
            Assert.Equal("UserUsernameTest", result.Username);
            Assert.Equal("UserPasswordTest", result.Password);
            Assert.Equal("UserEmailTest@test.dk", result.Email);
            Assert.Equal("UserUserTypeTest", result.UserType);
        }

        [Fact]
        public async void GetUserById_ShouldReturnNull_WhenUserDoesNotExist()
        {
            // Arrange
            int userId = 1;

            _mockUserRepository
                .Setup(x => x.SelectUserById(It.IsAny<int>()))
                .ReturnsAsync(() => null);

            // Act
            var result = await _userService.GetUserById(userId);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async void UpdateUser_ShouldReturnSingleUserResponse_WhenUserExists()
        {
            // Arrange
            int userId = 1;

            _mockUserRepository
                .Setup(x => x.UpdateExistingUserById(It.IsAny<int>(), It.IsAny<User>()))
                .ReturnsAsync(User());

            // Act
            var result = await _userService.UpdateUser(userId, UserRequest());

            // Assert
            Assert.NotNull(result);
            Assert.IsType<UserResponse>(result);
            Assert.Equal(userId, result.Id);
            Assert.Equal("UserUsernameTest", result.Username);
            Assert.Equal("UserPasswordTest", result.Password);
            Assert.Equal("UserEmailTest@test.dk", result.Email);
            Assert.Equal("UserUserTypeTest", result.UserType);
        }

        [Fact]
        public async void UpdateUser_ShouldReturnNull_WhenUserDoesNotExist()
        {
            // Arrange
            int userId = 1;

            _mockUserRepository
                .Setup(x => x.UpdateExistingUserById(It.IsAny<int>(), It.IsAny<User>()))
                .ReturnsAsync(() => null);

            // Act
            var result = await _userService.UpdateUser(userId, UserRequest());

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async void CreateUser_ShouldReturnUserResponse_WhenUserIsCreated()
        {
            // Arrange
            int userId = 1;

            _mockUserRepository
                .Setup(x => x.InsertNewUser(It.IsAny<User>()))
                .ReturnsAsync(User());

            // Act
            var result = await _userService.CreateUser(UserRequest());

            // Assert
            Assert.NotNull(result);
            Assert.IsType<UserResponse>(result);
            Assert.Equal(userId, result.Id);
            Assert.Equal("UserUsernameTest", result.Username);
            Assert.Equal("UserPasswordTest", result.Password);
            Assert.Equal("UserEmailTest@test.dk", result.Email);
            Assert.Equal("UserUserTypeTest", result.UserType);
        }

        [Fact]
        public async void CreateUser_ShouldReturnNull_WhenUserIsNotCreated()
        {
            // Arrange
            _mockUserRepository
                .Setup(x => x.InsertNewUser(It.IsAny<User>()))
                .ReturnsAsync(() => null);

            // Act
            var result = await _userService.CreateUser(UserRequest());

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async void DeleteUser_ShouldReturnUserResponse_WhenUserIsDeleted()
        {
            // Arrange
            int userId = 1;

            _mockUserRepository
                .Setup(x => x.DeleteUserById(It.IsAny<int>()))
                .ReturnsAsync(User());

            // Act
            var result = await _userService.DeleteUser(userId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<UserResponse>(result);
            Assert.Equal(userId, result.Id);
            Assert.Equal("UserUsernameTest", result.Username);
            Assert.Equal("UserPasswordTest", result.Password);
            Assert.Equal("UserEmailTest@test.dk", result.Email);
            Assert.Equal("UserUserTypeTest", result.UserType);
        }

        [Fact]
        public async void DeleteUser_ShouldReturnNull_WhenUserDoesNotExist()
        {
            // Arrange
            int userId = 1;

            _mockUserRepository
                .Setup(x => x.DeleteUserById(It.IsAny<int>()))
                .ReturnsAsync(() => null);

            // Act
            var result = await _userService.DeleteUser(userId);

            // Assert
            Assert.Null(result);
        }

        private User User()
        {
            return new User
            {
                Id = 1,
                Username = "UserUsernameTest",
                Password = "UserPasswordTest",
                Email = "UserEmailTest@test.dk",
                UserType = "UserUserTypeTest"
            };
        }

        private List<User> UserList()
        {
            return new List<User>()
            {
                new User
                {
                    Id = 1,
                    Username = "UserUsernameTest",
                    Password = "UserPasswordTest",
                    Email = "UserEmailTest@test.dk",
                    UserType = "UserUserTypeTest"
                },
                new User
                {
                    Id = 2,
                    Username = "UserUsernameTest2",
                    Password = "UserPasswordTest2",
                    Email = "UserEmailTest2@test.dk",
                    UserType = "UserUserTypeTest2"
                }
            };
        }

        private UserRequest UserRequest()
        {
            return new UserRequest
            {
                Username = "UserUsernameTest",
                Password = "UserPasswordTest",
                Email = "UserEmailTest@test.dk",
                UserType = "UserUserTypeTest"
            };
        }
    }
}
