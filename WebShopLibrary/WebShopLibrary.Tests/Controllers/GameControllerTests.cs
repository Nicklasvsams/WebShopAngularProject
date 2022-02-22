using BusinessAccessLayer.Services.GameServices;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Moq;
using System;
using System.Collections.Generic;
using WebApi.Controllers;
using WebShopLibrary.BusinessAccessLayer.DTOs.GameDTO;
using Xunit;

namespace WebShopLibrary.Tests.Controllers
{
    public class GameControllerTests
    {
        private readonly GameController _gameController;
        private readonly Mock<IGameService> _mockGameService = new Mock<IGameService>();

        public GameControllerTests()
        {
            _gameController = new GameController(_mockGameService.Object);
        }

        [Fact]
        public async void GetAll_ShouldReturnStatusCode204_WhenNoGamesExist()
        {
            // Arrange
            List<GameResponse> games = new List<GameResponse>();

            _mockGameService
                .Setup(x => x.GetAllGames())
                .ReturnsAsync(games);

            // Act
            var result = await _gameController.GetAll();
            var statusCodeResult = (IStatusCodeActionResult)result;

            // Assert
            Assert.Equal(204, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void GetAll_ShouldReturnStatusCode200_WhenGamesExist()
        {
            // Arrange
            List<GameResponse> games = new List<GameResponse>();

            games.AddRange(GameResponseList());

            _mockGameService
                .Setup(x => x.GetAllGames())
                .ReturnsAsync(games);

            // Act
            var result = await _gameController.GetAll();
            var statusCodeResult = (IStatusCodeActionResult)result;

            // Assert
            Assert.Equal(200, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void GetAll_ShouldReturnStatusCode500_WhenServiceReturnsNull()
        {
            // Arrange
            List<GameResponse> games = new List<GameResponse>();

            _mockGameService
                .Setup(x => x.GetAllGames())
                .ReturnsAsync(() => null);

            // Act
            var result = await _gameController.GetAll();
            var statusCodeResult = (IStatusCodeActionResult)result;

            // Assert
            Assert.Equal(500, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void GetAll_ShouldReturnStatusCode500_WhenExceptionIsRaised()
        {
            // Arrange
            _mockGameService
                .Setup(x => x.GetAllGames())
                .ReturnsAsync(() => throw new Exception("test"));

            // Act
            var result = await _gameController.GetAll();
            var statusCodeResult = (IStatusCodeActionResult)result;

            // Assert
            Assert.Equal(500, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void GetById_ShouldReturnStatusCode200_WhenGameExists()
        {
            // Arrange
            int gameId = 1;

            _mockGameService
                .Setup(x => x.GetGameById(It.IsAny<int>()))
                .ReturnsAsync(GameResponse());

            // Act
            var result = await _gameController.GetById(gameId);
            var statusCodeResult = (IStatusCodeActionResult)result;

            // Assert
            Assert.Equal(200, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void GetById_ShouldReturnStatusCode500_WhenExceptionIsRaised()
        {
            // Arrange
            int gameId = 1;

            _mockGameService
                .Setup(x => x.GetGameById(It.IsAny<int>()))
                .ReturnsAsync(() => throw new Exception("Test"));

            // Act
            var result = await _gameController.GetById(gameId);
            var statusCodeResult = (IStatusCodeActionResult)result;

            // Assert
            Assert.Equal(500, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void GetById_ShouldReturnStatusCode404_WhenGameDoesNotExist()
        {
            // Arrange
            int gameId = 1;

            _mockGameService
                .Setup(x => x.GetGameById(gameId))
                .ReturnsAsync(() => null);

            // Act
            var result = await _gameController.GetById(gameId);
            var statusCodeResult = (IStatusCodeActionResult)result;

            // Assert
            Assert.Equal(404, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void Create_ShouldReturnStatusCode200_WhenGameIsCreated()
        {
            // Arrange
            _mockGameService
                .Setup(x => x.CreateGame(It.IsAny<GameRequest>()))
                .ReturnsAsync(GameResponse());

            // Act
            var result = await _gameController.Create(GameRequest());
            var statusCodeResult = (IStatusCodeActionResult)result;

            // Assert
            Assert.Equal(200, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void Create_ShouldReturnStatusCode500_WhenExceptionIsRaised()
        {
            // Arrange
            _mockGameService
                .Setup(x => x.CreateGame(It.IsAny<GameRequest>()))
                .ReturnsAsync(() => throw new Exception("Test"));

            // Act
            var result = await _gameController.Create(GameRequest());
            var statusCodeResult = (IStatusCodeActionResult)result;

            // Assert
            Assert.Equal(500, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void Update_ShouldReturnStatusCode200_WhenUpdateIsSuccessful()
        {
            // Arrange
            int gameId = 1;

            _mockGameService
                .Setup(x => x.UpdateGame(It.IsAny<int>(), It.IsAny<GameRequest>()))
                .ReturnsAsync(GameResponse());

            // Act
            var result = await _gameController.Update(gameId, GameRequest());
            var statusCodeResult = (IStatusCodeActionResult)result;

            // Assert
            Assert.Equal(200, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void Update_ShouldReturnStatusCode404_WhenGameToUpdateIsNotFound()
        {
            // Arrange
            int gameId = 1;

            _mockGameService
                .Setup(x => x.UpdateGame(It.IsAny<int>(), It.IsAny<GameRequest>()))
                .ReturnsAsync(() => null);

            // Act
            var result = await _gameController.Update(gameId, GameRequest());
            var statusCodeResult = (IStatusCodeActionResult)result;

            // Assert
            Assert.Equal(404, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void Update_ShouldReturnStatusCode500_WhenExceptionIsRaised()
        {
            // Arrange
            int gameId = 1;

            _mockGameService
                .Setup(x => x.UpdateGame(It.IsAny<int>(), It.IsAny<GameRequest>()))
                .ReturnsAsync(() => throw new Exception("Test"));

            // Act
            var result = await _gameController.Update(gameId, GameRequest());
            var statusCodeResult = (IStatusCodeActionResult)result;

            // Assert
            Assert.Equal(500, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void Delete_ShouldReturnStatusCode200_WhenDeleteIsSuccessful()
        {
            // Arrange
            int gameId = 1;

            _mockGameService
                .Setup(x => x.DeleteGame(It.IsAny<int>()))
                .ReturnsAsync(GameResponse());

            // Act
            var result = await _gameController.Delete(gameId);
            var statusCodeResult = (IStatusCodeActionResult)result;

            // Assert
            Assert.Equal(200, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void Delete_ShouldReturnStatusCode404_WhenGameToDeleteIsNotFound()
        {
            // Arrange
            int gameId = 1;

            _mockGameService
                .Setup(x => x.DeleteGame(It.IsAny<int>()))
                .ReturnsAsync(() => null);

            // Act
            var result = await _gameController.Delete(gameId);
            var statusCodeResult = (IStatusCodeActionResult)result;

            // Assert
            Assert.Equal(404, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void Delete_ShouldReturnStatusCode500_WhenExceptionIsRaised()
        {
            // Arrange
            int gameId = 1;

            _mockGameService
                .Setup(x => x.DeleteGame(It.IsAny<int>()))
                .ReturnsAsync(() => throw new Exception("Test"));

            // Act
            var result = await _gameController.Delete(gameId);
            var statusCodeResult = (IStatusCodeActionResult)result;

            // Assert
            Assert.Equal(500, statusCodeResult.StatusCode);
        }

        private GameResponse GameResponse()
        {
            return new GameResponse()
            {
                Id = 1,
                Genre = "Test",
                Language = "Test",
                PublishedYear = 1991,
                Publisher = "Test",
                Category = new GameCategoryResponse(),
                Product = new GameProductResponse()
            };
        }

        private GameRequest GameRequest()
        {
            return new GameRequest()
            {
                Genre = "Test",
                Language = "Test",
                PublishedYear = 1991,
                Publisher = "Test",
            };
        }

        private List<GameResponse> GameResponseList()
        {
            return new List<GameResponse>()
            {
                new GameResponse()
                {
                    Id = 1,
                    Genre = "Test",
                    Language = "Test",
                    PublishedYear = 1991,
                    Publisher = "Test",
                    Category = new GameCategoryResponse(),
                    Product = new GameProductResponse()
                },
                new GameResponse()
                {
                    Id = 2,
                    Genre = "Test2",
                    Language = "Test2",
                    PublishedYear = 2000,
                    Publisher = "Test2",
                    Category = new GameCategoryResponse(),
                    Product = new GameProductResponse()
                }
            };
        }
    }
}
