using System.Collections.Generic;
using System.Threading.Tasks;
using WebShopLibrary.BusinessAccessLayer.DTOs.GameDTO;
using WebShopLibrary.DataAccessLayer.Database.Entities.Products.Games;
using System.Linq;
using WebShopLibrary.DataAccessLayer.Repositories.ProductRepositories;

namespace BusinessAccessLayer.Services.GameServices
{
    public interface IGameService
    {
        Task<List<GameResponse>> GetAllGames();
        Task<GameResponse> GetGameById(int gameId);
        Task<GameResponse> CreateGame(GameRequest newGame);
        Task<GameResponse> DeleteGame(int gameId);
        Task<GameResponse> UpdateGame(int gameId, GameRequest gameUpdate);

    }

    public class GameService : IGameService
    {
        private readonly IGameRepository _gameRepository;

        public GameService(IGameRepository gameRepository)
        {
            _gameRepository = gameRepository;
        }

        public async Task<GameResponse> CreateGame(GameRequest newGame)
        {
            Game game = MapGameRequestToGame(newGame);

            Game insertedGame = await _gameRepository.InsertNewGame(game);

            if(insertedGame != null)
            {
                return MapGameToGameResponse(insertedGame);
            }

            return null;
        }

        public async Task<GameResponse> DeleteGame(int gameId)
        {
            Game deletedGame = await _gameRepository.DeleteGameById(gameId);

            if(deletedGame != null)
            {
                return MapGameToGameResponse(deletedGame);
            }

            return null;
        }

        public async Task<List<GameResponse>> GetAllGames()
        {
            List<Game> games = await _gameRepository.SelectAllGames();

            return games.Select(game => MapGameToGameResponse(game)).ToList();
        }

        public async Task<GameResponse> GetGameById(int gameId)
        {
            Game gameById = await _gameRepository.SelectGameById(gameId);

            if (gameById != null)
            {
                return MapGameToGameResponse(gameById);
            }

            return null;
        }

        public async Task<GameResponse> UpdateGame(int gameId, GameRequest gameUpdate)
        {
            Game gameToUpdate = await _gameRepository.UpdateExistingGame(gameId, MapGameRequestToGame(gameUpdate));

            if (gameToUpdate != null)
            {
                return MapGameToGameResponse(gameToUpdate);
            }

            return null;
        }

        private static GameResponse MapGameToGameResponse(Game game)
        {
            GameResponse gameRes = new GameResponse
            {
                Id = game.Id,
                Publisher = game.Publisher,
                PublishedYear = game.PublishedYear,
                Language = game.Language,
                Genre = game.Genre
            };

            if (game.Product != null && game.Publisher != null)
            {

                gameRes.Category = new GameCategoryResponse
                {
                    Id = game.Category.Id,
                    Name = game.Category.Name,
                    Description = game.Category.Description
                };
                gameRes.Product = new GameProductResponse
                {
                    Id = game.Product.Id,
                    Price = game.Product.Price,
                    Name = game.Product.Name,
                    Description = game.Product.Description,
                    Stock = game.Product.Stock
                };
            }
            else
            {
                gameRes.Category = new GameCategoryResponse();
                gameRes.Product = new GameProductResponse();
            }

            return gameRes;
        }

        private static Game MapGameRequestToGame(GameRequest gameRequest)
        {
            return new Game
            {
                Publisher = gameRequest.Publisher,
                PublishedYear = gameRequest.PublishedYear,
                Language = gameRequest.Language,
                Genre = gameRequest.Genre,
                CategoryId = gameRequest.CategoryId,
                ProductId = gameRequest.ProductId
            };
        }
    }
}
