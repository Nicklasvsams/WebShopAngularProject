using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebShopLibrary.BusinessAccessLayer.DTOs.GameDTO;
using WebShopLibrary.DataAccessLayer.Database;
using WebShopLibrary.DataAccessLayer.Database.Entities.Products;
using WebShopLibrary.DataAccessLayer.Database.Entities.Products.Games;
using WebShopLibrary.DataAccessLayer.Repositories.ProductRepositories;
using Xunit;

namespace WebShopLibrary.Tests.Repositories
{
    public class GameRepositoryTests
    {
        private readonly DbContextOptions<WebShopDBContext> _options;
        private readonly WebShopDBContext _context;
        private readonly GameRepository _gameRepository;

        public GameRepositoryTests()
        {
            _options = new DbContextOptionsBuilder<WebShopDBContext>()
                .UseInMemoryDatabase(databaseName: "WebShopGame")
                .Options;
            _context = new WebShopDBContext(_options);
            _gameRepository = new GameRepository(_context);
        }

        [Fact]
        public async void SelectAllGames_ShouldReturnListOfGames_WhenGamesExist()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();

            _context.Game.Add(GameList(1,2)[0]);
            _context.Game.Add(GameList(1,2)[1]);

            await _context.SaveChangesAsync();

            // Act
            var result = await _gameRepository.SelectAllGames();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<Game>>(result);
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async void SelectAllGames_ShouldReturnEmptyListOfGames_WhenNoGamesExist()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();

            // Act
            var result = await _gameRepository.SelectAllGames();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<Game>>(result);
            Assert.Empty(result);
        }

        [Fact]
        public async void DeleteGameById_ShouldReturnDeletedGame_WhenGameExists()
        {
            // Arrange
            int gameId = 1;

            await _context.Database.EnsureDeletedAsync();

            _context.Game.Add(GameList(1, 2)[0]);
            _context.Game.Add(GameList(1, 2)[1]);

            await _context.SaveChangesAsync();

            // Act
            var result = await _gameRepository.DeleteGameById(gameId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<Game>(result);
            Assert.Equal(gameId, result.Id);
        }

        [Fact]
        public async void DeleteGameById_ShouldReturnNull_WhenGameDoesNotExist()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();

            // Act
            var result = await _gameRepository.DeleteGameById(1);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async void SelectGameById_ShouldReturnGame_WhenGameExists()
        {
            // Arrange
            int gameId = 1;

            await _context.Database.EnsureDeletedAsync();

            _context.Game.Add(Game(gameId));

            await _context.SaveChangesAsync();

            // Act
            var result = await _gameRepository.SelectGameById(gameId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<Game>(result);
            Assert.Equal(gameId, result.Id);
            Assert.Equal("TestPublisher", result.Publisher);
            Assert.Equal(1991, result.PublishedYear);
            Assert.Equal("TestLanguage", result.Language);
            Assert.Equal("TestGenre", result.Genre);
            Assert.IsType<Category>(result.Category);
            Assert.IsType<Product>(result.Product);
        }

        [Fact]
        public async void SelectGameById_ShouldReturnNull_WhenGameDoesNotExist()
        {
            // Arrange
            int gameId = 1;

            await _context.Database.EnsureDeletedAsync();

            // Act
            var result = await _gameRepository.SelectGameById(gameId);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async void InsertNewGame_ShouldAddIdAndReturnGame_WhenGameIsSuccessfullyCreated()
        {
            // Arrange
            int gameId = 1;

            await _context.Database.EnsureDeletedAsync();

            Game game = new Game(){
                Publisher = "TestPublisher",
                PublishedYear = 1991,
                Language = "TestLanguage"
            };

            // Act
            var result = await _gameRepository.InsertNewGame(game);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<Game>(result);
            Assert.Equal(gameId, result.Id);
            Assert.Equal("TestLanguage", result.Language);
        }

        [Fact]
        public async void InsertNewGame_ShouldFailToAddGame_WhenGameWithSameIdAlreadyExists()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();

            Game game = new Game
            {
                Id = 1,
                Publisher = "TestPublisher",
                PublishedYear = 1991,
                Language = "TestLanguage",
                Genre = "TestGenre",
                CategoryId = 1,
                ProductId = 1,
                Category = new Category(),
                Product = new Product(),
            };

            _context.Add(game);

            await _context.SaveChangesAsync();

            // Act
            async Task action() => await _gameRepository.InsertNewGame(game);

            // Assert
            var ex = await Assert.ThrowsAsync<ArgumentException>(action);
            Assert.Contains("An item with the same key has already been added", ex.Message);
        }

        [Fact]
        public async void UpdateExistingGame_ShouldReturnGame_WhenGameExists()
        {
            // Arrange
            int gameId = 1;

            await _context.Database.EnsureDeletedAsync();

            _context.Game.Add(Game(gameId));

            await _context.SaveChangesAsync();

            Game game = new Game()
            {
                Publisher = "TestPublisher123",
                PublishedYear = 1995,
                Language = "TestLanguage123",
                Genre = "TestGenre123",
                CategoryId = 5,
                ProductId = 7,
            };

            // Act
            var result = await _gameRepository.UpdateExistingGame(gameId, game);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<Game>(result);
            Assert.Equal(gameId, result.Id);
            Assert.Equal("TestPublisher123", result.Publisher);
            Assert.Equal(1995, result.PublishedYear);
            Assert.Equal("TestLanguage123", result.Language);
            Assert.Equal("TestGenre123", result.Genre);
            Assert.Equal(5, result.CategoryId);
            Assert.Equal(7, result.ProductId);
        }

        [Fact]
        public async void UpdateExistingGame_ShouldReturnNull_WhenGameToUpdateDoesNotExist()
        {
            // Arrange
            int gameId = 1;

            await _context.Database.EnsureDeletedAsync();

            Game game = new Game()
            {
                Publisher = "TestPublisher2",
                PublishedYear = 1991,
                Language = "TestLanguage2"
            };

            // Act
            var result = await _gameRepository.UpdateExistingGame(gameId, game);

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
                CategoryId = 1,
                ProductId = 1,
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
                    CategoryId = 1,
                    ProductId = 1,
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
                    CategoryId = 2,
                    ProductId = 2,
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
                CategoryId = 2,
                ProductId = 2
            };
        }
    }
}
