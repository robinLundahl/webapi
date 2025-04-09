using Entities.Models;
using Entitites.DTOs.User;

namespace webapi.Repositories;
public interface IUserRepository
{
    public Task<List<UserDTO>> GetAsync();
    public Task<UserDTO?> GetAsync(Guid id);
    public Task<User> GetAsyncIncludePassword(Guid id);
    public Task<UserDTO> CreateAsync(User user);
    public Task<UserDTO> UpdateAsync(User user);
    public Task<User> DeleteAsync(User user);
}