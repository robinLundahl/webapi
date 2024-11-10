using Entities.Models;
using Entitites.DTOs.User;

namespace webapi.Service.UserInterface;

public interface IUserService
{
    Task<List<UserDTO>> GetAllUsersAsync();
    Task<UserDTO?> GetUserByIdAsync(int id);
    Task<UserDTO?> AddUserAsync(User user);
    Task<User?> DeleteUserAsync(int id);
    Task<User?> UpdateUserAsync(int id, User userToUpdate);
}