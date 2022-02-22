using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebShopLibrary.DataAccessLayer.Database;
using WebShopLibrary.DataAccessLayer.Database.Entities.Products.Games;

namespace WebShopLibrary.DataAccessLayer.Repositories.ProductRepositories
{
    public interface IGameRepository
    {
        Task<List<Game>> SelectAllGames();
        Task<Game> SelectGameById(int gameId);
        Task<Game> InsertNewGame(Game game);
        Task<Game> UpdateExistingGame(int gameId, Game game);
        Task<Game> DeleteGameById(int gameId);
    }

    public class GameRepository : IGameRepository
    {
        private readonly WebShopDBContext _dbContext;

        public GameRepository(WebShopDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Game> DeleteGameById(int gameId)
        {
            Game gameToDelete = await _dbContext.Game.FirstOrDefaultAsync(x => x.Id == gameId);

            if (gameToDelete != null)
            {
                _dbContext.Game.Remove(gameToDelete);

                await _dbContext.SaveChangesAsync();
            }

            return gameToDelete;
        }

        public async Task<Game> InsertNewGame(Game game)
        {
            await _dbContext.Game.AddAsync(game);
            await _dbContext.SaveChangesAsync();

            return game;
        }

        public async Task<List<Game>> SelectAllGames()
        {
            return await _dbContext.Game
                .Include(p => p.Product)
                .Include(c => c.Category)
                .ToListAsync();
        }

        public async Task<Game> SelectGameById(int gameId)
        {
            return await _dbContext.Game
                .Include(p => p.Product)
                .Include(c => c.Category)
                .FirstOrDefaultAsync(x => x.Id == gameId);
        }

        public async Task<Game> UpdateExistingGame(int gameId, Game game)
        {
            Game gameToUpdate = await _dbContext.Game.FirstOrDefaultAsync(x => x.Id == gameId);

            if (gameToUpdate != null)
            {
                gameToUpdate.Publisher = game.Publisher;
                gameToUpdate.PublishedYear = game.PublishedYear;
                gameToUpdate.Language = game.Language;
                gameToUpdate.ProductId = game.ProductId;
                gameToUpdate.CategoryId = game.CategoryId;
                gameToUpdate.Genre = game.Genre;
                gameToUpdate.Product = game.Product;
                gameToUpdate.Category = game.Category;

                await _dbContext.SaveChangesAsync();
            }

            return gameToUpdate;
        }
    }
}
