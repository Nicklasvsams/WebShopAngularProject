using BusinessAccessLayer.Services.GameServices;
using Moq;
using System.Collections.Generic;
using WebShopLibrary.BusinessAccessLayer.DTOs.GameDTO;
using WebShopLibrary.DataAccessLayer.Database.Entities.Products;
using WebShopLibrary.DataAccessLayer.Database.Entities.Products.Games;
using WebShopLibrary.DataAccessLayer.Repositories.ProductRepositories;
using Xunit;

namespace WebShopLibrary.Tests.Services
{
    public class GameServiceTests
    {
        private readonly GameService _gameService;
        private readonly Mock<IGameRepository> _mockGameRepository = new Mock<IGameRepository>();

        public GameServiceTests()
        {
            _gameService = new GameService(_mockGameRepository.Object);
        }

        [Fact]
        public async void GetAllGames_ShouldReturnListOfGameResponses_WhenGamesExist()
        {
            // Arrange
            int gameId1 = 1;
            int gameId2 = 2;

            _mockGameRepository
                .Setup(x => x.SelectAllGames())
                .ReturnsAsync(GameList(gameId1, gameId2));

            // Act
            var result = await _gameService.GetAllGames();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<GameResponse>>(result);
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async void GetAllGames_ShouldReturnEmptyListOfGameResponses_WhenNoGamesExist()
        {
            // Arrange
            List<Game> games = new List<Game>();

            _mockGameRepository
                .Setup(x => x.SelectAllGames())
                .ReturnsAsync(games);

            // Act
            var result = await _gameService.GetAllGames();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<GameResponse>>(result);
            Assert.Empty(result);
        }

        [Fact]
        public async void GetGameById_ShouldReturnSingleGameResponse_WhenGameExists()
        {
            // Arrange
            int gameId = 1;

            _mockGameRepository
                .Setup(x => x.SelectGameById(It.IsAny<int>()))
                .ReturnsAsync(Game(gameId));

            // Act
            var result = await _gameService.GetGameById(gameId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<GameResponse>(result);
            Assert.Equal(gameId, result.Id);
            Assert.Equal("TestPublisher", result.Publisher);
            Assert.Equal(1991, result.PublishedYear);
            Assert.Equal("TestLanguage", result.Language);
            Assert.Equal("TestGenre", result.Genre);
            Assert.IsType<GameCategoryResponse>(result.Category);
            Assert.IsType<GameProductResponse>(result.Product);
        }

        [Fact]
        public async void GetGameById_ShouldReturnNull_WhenGameDoesNotExist()
        {
            // Arrange
            int gameId = 1;

            _mockGameRepository
                .Setup(x => x.SelectGameById(It.IsAny<int>()))
                .ReturnsAsync(() => null);

            // Act
            var result = await _gameService.GetGameById(gameId);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async void UpdateGame_ShouldReturnSingleGameResponse_WhenGameExists()
        {
            // Arrange
            int gameId = 1;

            _mockGameRepository
                .Setup(x => x.UpdateExistingGame(It.IsAny<int>(), It.IsAny<Game>()))
                .ReturnsAsync(Game(gameId));

            // Act
            var result = await _gameService.UpdateGame(gameId, GameRequest());

            // Assert
            Assert.NotNull(result);
            Assert.IsType<GameResponse>(result);
            Assert.Equal(gameId, result.Id);
            Assert.Equal(1991, result.PublishedYear);
        }

        [Fact]
        public async void UpdateGame_ShouldReturnNull_WhenGameDoesNotExist()
        {
            // Arrange
            int gameId = 1;

            _mockGameRepository
                .Setup(x => x.UpdateExistingGame(It.IsAny<int>(), It.IsAny<Game>()))
                .ReturnsAsync(() => null);

            // Act
            var result = await _gameService.UpdateGame(gameId, GameRequest());

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async void CreateGame_ShouldReturnGameResponse_WhenGameIsCreated()
        {
            // Arrange
            int gameId = 1;

            _mockGameRepository
                .Setup(x => x.InsertNewGame(It.IsAny<Game>()))
                .ReturnsAsync(Game(gameId));

            // Act
            var result = await _gameService.CreateGame(GameRequest());

            // Assert
            Assert.NotNull(result);
            Assert.IsType<GameResponse>(result);
            Assert.Equal(gameId, result.Id);
            Assert.Equal("TestLanguage", result.Language);
        }

        [Fact]
        public async void CreateGame_ShouldReturnNull_WhenGameIsNotCreated()
        {
            // Arrange
            _mockGameRepository
                .Setup(x => x.InsertNewGame(It.IsAny<Game>()))
                .ReturnsAsync(() => null);

            // Act
            var result = await _gameService.CreateGame(GameRequest());

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async void DeleteGame_ShouldReturnGameResponse_WhenGameIsDeleted()
        {
            // Arrange
            int gameId = 1;

            _mockGameRepository
                .Setup(x => x.DeleteGameById(It.IsAny<int>()))
                .ReturnsAsync(Game(gameId));

            // Act
            var result = await _gameService.DeleteGame(gameId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<GameResponse>(result);
            Assert.Equal(gameId, result.Id);
            Assert.Equal("TestLanguage", result.Language);
        }

        [Fact]
        public async void DeleteGame_ShouldReturnNull_WhenGameDoesNotExist()
        {
            // Arrange
            int gameId = 1;

            _mockGameRepository
                .Setup(x => x.DeleteGameById(It.IsAny<int>()))
                .ReturnsAsync(() => null);

            // Act
            var result = await _gameService.DeleteGame(gameId);

            // Assert
            Assert.Null(result);
        }

        private Game Game(int gameId)
        {
            return new Game
            {
                Id = gameId,
                Publisher = "TestPublisher",
                PublishedYear = 1991,
                Language = "TestLanguage",
                Genre = "TestGenre",
                Category = new Category(),
                Product = new Product(),
            };
        }

        private List<Game> GameList(int gameId1, int gameId2)
        {
            return new List<Game>()
            {
                new Game
                {
                    Id = gameId1,
                    Publisher = "TestPublisher",
                    PublishedYear = 1991,
                    Language = "TestLanguage",
                    Genre = "TestGenre",
                    Category = new Category(),
                    Product = new Product(),
                },
                new Game
                {
                    Id = gameId2,
                    Publisher = "TestPublisher",
                    PublishedYear = 1991,
                    Language = "TestLanguage",
                    Genre = "TestGenre",
                    Category = new Category(),
                    Product = new Product(),
                }
            };
        }

        private GameRequest GameRequest()
        {
            return new GameRequest
            {
                Publisher = "TestPublisher",
                PublishedYear = 1991,
                Language = "TestLanguage",
                Genre = "TestGenre",
                CategoryId = 1,
                ProductId = 1
            };
        }
    }
}
