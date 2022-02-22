using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebShopLibrary.BusinessAccessLayer.DTOs.UserDTO;
using WebShopLibrary.DataAccessLayer.Database.Entities.Users;
using WebShopLibrary.DataAccessLayer.Repositories.UserRepositories;

namespace WebShopLibrary.BusinessAccessLayer.Services.UserServices
{
    public interface IUserService
    {
        Task<List<UserResponse>> GetAllUsers();
        Task<UserResponse> GetUserById(int userId);
        Task<UserResponse> DeleteUser(int userId);
        Task<UserResponse> UpdateUser(int userId, UserRequest userUpdate);
        Task<UserResponse> CreateUser(UserRequest userToCreate);
    }

    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserResponse> CreateUser(UserRequest userToCreate)
        {
            User createdUser = await _userRepository.InsertNewUser(MapUserRequestToUser(userToCreate));

            if (createdUser != null)
            {
                return MapUserToUserResponse(createdUser);
            }

            return null;
        }

        public async Task<UserResponse> DeleteUser(int userId)
        {
            User user = await _userRepository.DeleteUserById(userId);

            if (user != null)
            {
                return MapUserToUserResponse(user);
            }

            return null;
        }

        public async Task<List<UserResponse>> GetAllUsers()
        {
            List<User> users = await _userRepository.SelectAllUsers();

            return users.Select(x => MapUserToUserResponse(x)).ToList();
        }

        public async Task<UserResponse> GetUserById(int userId)
        {
            User user = await _userRepository.SelectUserById(userId);

            if (user != null)
            {
                return MapUserToUserResponse(user);
            }

            return null;
        }

        public async Task<UserResponse> UpdateUser(int userId, UserRequest userUpdate)
        {
            User userToUpdate = await _userRepository.UpdateExistingUserById(userId, MapUserRequestToUser(userUpdate));

            if (userToUpdate != null)
            {
                return MapUserToUserResponse(userToUpdate);
            }

            return null;
        }

        public UserResponse MapUserToUserResponse(User user)
        {
            return new UserResponse
            {
                Id = user.Id,
                Username = user.Username,
                Password = user.Password,
                Email = user.Email,
                UserType = user.UserType
            };
        }

        public User MapUserRequestToUser(UserRequest userReq)
        {
            return new User
            {
                Username = userReq.Username,
                Password = userReq.Password,
                Email = userReq.Email,
                UserType = userReq.UserType
            };
        }
    }
}
