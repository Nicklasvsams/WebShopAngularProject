using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebShopLibrary.DataAccessLayer.Database;
using WebShopLibrary.DataAccessLayer.Database.Entities.Users;

namespace WebShopLibrary.DataAccessLayer.Repositories.UserRepositories
{
    public interface IUserRepository
    {
        Task<List<User>> SelectAllUsers();
        Task<User> SelectUserById(int userId);
        Task<User> InsertNewUser(User user);
        Task<User> DeleteUserById(int userId);
        Task<User> UpdateExistingUserById(int userId, User userUpdate);
    }

    public class UserRepository : IUserRepository
    {
        private readonly WebShopDBContext _dbContext;

        public UserRepository(WebShopDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<User> InsertNewUser(User user)
        {
            await _dbContext.User.AddAsync(user);
            await _dbContext.SaveChangesAsync();

            return user;
        }

        public async Task<User> DeleteUserById(int userId)
        {
            User userToDelete = await _dbContext.User.FirstOrDefaultAsync(x => x.Id == userId);

            if (userToDelete != null)
            {
                _dbContext.Remove(userToDelete);

                await _dbContext.SaveChangesAsync();
            }

            return userToDelete;
        }

        public async Task<List<User>> SelectAllUsers()
        {
            return await _dbContext.User.ToListAsync();
        }

        public async Task<User> SelectUserById(int userId)
        {
            return await _dbContext.User
                .FirstOrDefaultAsync(x => x.Id == userId);
        }

        public async Task<User> UpdateExistingUserById(int userId, User userUpdate)
        {
            User userToUpdate = await _dbContext.User
                .FirstOrDefaultAsync(x => x.Id == userId);

            if (userToUpdate != null)
            {
                userToUpdate.Username = userUpdate.Username;
                userToUpdate.Password = userUpdate.Password;
                userToUpdate.UserType = userUpdate.UserType;

                await _dbContext.SaveChangesAsync();
            }

            return userToUpdate;
        }
    }
}
