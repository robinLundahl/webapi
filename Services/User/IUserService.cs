using Entities.Models;
using Entitites.DTOs.User;

namespace webapi.Service.UserInterface;

public interface IUserService
{
    Task<List<UserDTO>> GetAsync();
    Task<UserDTO?> GetAsync(Guid id);
    Task<UserDTO?> CreateAsync(User user);
    Task<UserDTO?> UpdateAsync(Guid id, User userToUpdate);
    Task<User?> DeleteAsync(User user);
}