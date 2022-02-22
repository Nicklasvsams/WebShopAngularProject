using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebShopLibrary.DataAccessLayer.Database;
using WebShopLibrary.DataAccessLayer.Database.Entities.Users;
using WebShopLibrary.DataAccessLayer.Repositories.UserRepositories;
using Xunit;

namespace WebShopLibrary.Tests.Repositories
{
    public class UserRepositoryTests
    {
        private readonly DbContextOptions<WebShopDBContext> _options;
        private readonly WebShopDBContext _context;
        private readonly UserRepository _userRepository;

        public UserRepositoryTests()
        {
            _options = new DbContextOptionsBuilder<WebShopDBContext>()
                .UseInMemoryDatabase(databaseName: "WebShopUser")
                .Options;
            _context = new WebShopDBContext(_options);
            _userRepository = new UserRepository(_context);
        }

        [Fact]
        public async void SelectAllUsers_ShouldReturnListOfUsers_WhenUsersExist()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();

            _context.User.AddRange(UserList());

            await _context.SaveChangesAsync();

            // Act
            var result = await _userRepository.SelectAllUsers();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<User>>(result);
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async void SelectAllUsers_ShouldReturnEmptyListOfUsers_WhenNoUsersExist()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();

            // Act
            var result = await _userRepository.SelectAllUsers();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<User>>(result);
            Assert.Empty(result);
        }

        [Fact]
        public async void DeleteUserById_ShouldReturnDeletedUser_WhenUserExists()
        {
            // Arrange
            int userId = 1;

            await _context.Database.EnsureDeletedAsync();

            _context.User.AddRange(UserList());

            await _context.SaveChangesAsync();

            // Act
            var result = await _userRepository.DeleteUserById(userId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<User>(result);
            Assert.Equal(userId, result.Id);
        }

        [Fact]
        public async void DeleteUserById_ShouldReturnNull_WhenUserDoesNotExist()
        {
            // Arrange
            int userId = 1;

            await _context.Database.EnsureDeletedAsync();

            // Act
            var result = await _userRepository.DeleteUserById(userId);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async void SelectUserById_ShouldReturnUser_WhenUserExists()
        {
            // Arrange
            int userId = 1;

            await _context.Database.EnsureDeletedAsync();

            _context.User.Add(User(userId));

            await _context.SaveChangesAsync();

            // Act
            var result = await _userRepository.SelectUserById(userId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<User>(result);
            Assert.Equal(userId, result.Id);
            Assert.Equal("TestUsername", result.Username);
            Assert.Equal("TestPassword", result.Password);
            Assert.Equal("TestUserType", result.UserType);
        }

        [Fact]
        public async void SelectUserById_ShouldReturnNull_WhenUserDoesNotExist()
        {
            // Arrange
            int userId = 1;

            await _context.Database.EnsureDeletedAsync();

            // Act
            var result = await _userRepository.SelectUserById(userId);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async void CreateUser_ShouldAddIdAndReturnUser_WhenUserIsSuccessfullyCreated()
        {
            // Arrange
            int userId = 1;

            await _context.Database.EnsureDeletedAsync();

            // Act
            var result = await _userRepository.InsertNewUser(User());

            // Assert
            Assert.NotNull(result);
            Assert.IsType<User>(result);
            Assert.Equal(userId, result.Id);
            Assert.Equal("TestUsername", result.Username);
            Assert.Equal("TestPassword", result.Password);
            Assert.Equal("TestUserType", result.UserType);
        }

        [Fact]
        public async void CreateUser_ShouldFailToAddUser_WhenUserWithSameIdAlreadyExists()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();

            User user = new User
            {
                Id = 1,
                Username = "TestUsername",
                Password = "TestPassword",
                UserType = "TestUserType"
            };

            _context.Add(user);

            await _context.SaveChangesAsync();

            // Act
            async Task action() => await _userRepository.InsertNewUser(user);

            // Assert
            var ex = await Assert.ThrowsAsync<ArgumentException>(action);
            Assert.Contains("An item with the same key has already been added", ex.Message);
        }

        [Fact]
        public async void UpdateExistingUser_ShouldReturnUser_WhenUserExists()
        {
            // Arrange
            int userId = 1;

            await _context.Database.EnsureDeletedAsync();

            _context.User.Add(User(userId));

            await _context.SaveChangesAsync();

            User user = new User
            {
                Id = 1,
                Username = "TestUsernameUpdate",
                Password = "TestPasswordUpdate",
                UserType = "TestUserTypeUpdate"
            };

            // Act
            var result = await _userRepository.UpdateExistingUserById(userId, user);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<User>(result);
            Assert.Equal(userId, result.Id);
            Assert.Equal("TestUsernameUpdate", result.Username);
            Assert.Equal("TestPasswordUpdate", result.Password);
            Assert.Equal("TestUserTypeUpdate", result.UserType);
        }

        [Fact]
        public async void UpdateExistingUser_ShouldReturnNull_WhenUserToUpdateDoesNotExist()
        {
            // Arrange
            int userId = 1;

            await _context.Database.EnsureDeletedAsync();

            User user = new User
            {
                Id = 1,
                Username = "TestUsernameUpdate",
                Password = "TestPasswordUpdate",
                UserType = "TestUserTypeUpdate"
            };

            // Act
            var result = await _userRepository.UpdateExistingUserById(userId, user);

            // Assert
            Assert.Null(result);
        }

        private User User()
        {
            return new User
            {
                Id = 1,
                Username = "TestUsername",
                Password = "TestPassword",
                UserType = "TestUserType"
            };
        }

        private User User(int userId)
        {
            return new User
            {
                Username = "TestUsername",
                Password = "TestPassword",
                UserType = "TestUserType"
            };
        }

        private List<User> UserList()
        {
            return new List<User>()
            {
                new User
                {
                    Id = 1,
                    Username = "TestUsername1",
                    Password = "TestPassword1",
                    UserType = "TestUserType1"
                },
                new User
                {
                    Id = 2,
                    Username = "TestUsername2",
                    Password = "TestPassword2",
                    UserType = "TestUserType2"
                }
            };
        }
    }
}
