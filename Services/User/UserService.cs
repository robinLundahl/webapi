using Entities.Models;
using Entitites.DTOs.User;
using Mapster;
using webapi.Service.UserInterface;
using webapi.Repositories;

namespace webapi.Service;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly ILogger<UserService> _logger;

    public UserService(IUserRepository userRepository, ILogger<UserService> logger)
    {
        _userRepository = userRepository;
        _logger = logger;
    }

    public async Task<List<UserDTO>> GetAsync()
    {
        try
        {
            var users = await _userRepository.GetAsync();
            return users.Adapt<List<UserDTO>>();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to retrieve the users.");
            throw;
        }
    }

    public async Task<UserDTO?> GetAsync(Guid id)
    {
        try
        {
            var user = await _userRepository.GetAsync(id);
            return user.Adapt<UserDTO>();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to retrieve the user with id: {id}.", id);
            throw;
        }
    }

    public async Task<UserDTO> CreateAsync(User user)
    {
        var existingUser = await _userRepository.GetAsync(user.Id);

        if (existingUser is not null)
            throw new Exception("The user already exists in the database.");

        return await _userRepository.CreateAsync(user);
    }

    public async Task<UserDTO?> UpdateAsync(Guid id, User userToUpdate)
    {
        var user = await _userRepository.GetAsyncIncludePassword(id);

        if (user != null)
        {
            user.FirstName = userToUpdate.FirstName;
            user.LastName = userToUpdate.LastName;
            user.Email = userToUpdate.Email;
            user.PhoneNumber = userToUpdate.PhoneNumber;
            user.Password = userToUpdate.Password;
            await _userRepository.UpdateAsync(user);

            return user.Adapt<UserDTO>();
        }
        return null;
    }

    public async Task<User> DeleteAsync(User user)
    {
        if (user != null)
        {
            await _userRepository.DeleteAsync(user);
        }
        return user;
    }
}