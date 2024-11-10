using Microsoft.EntityFrameworkCore;
using webapi.Infrastructor;
using Entities.Models;

namespace webapi.Service;

public class UserService
{
    private readonly TrubadurenContext _db;
    private readonly ILogger<UserService> _logger;

    public UserService(TrubadurenContext db, ILogger<UserService> logger)
    {
        _db = db;
        _logger = logger;
    }

    public async Task<User?> GetUserByIdAsync(int id)
    {
        var user = await _db.Users.FirstOrDefaultAsync(u => u.Id == id);
        return user;
    }

    public async Task<List<User>> GetAllUsersAsync()
    {
        try
        {
            return await _db.Users
             .OrderBy(u => u.Id).ToListAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to retrieve the users.");
            throw;
        }
    }

    public async Task<User?> AddUserAsync(User user)
    {
        string lowercaseEmail = user.Email.ToLower();

        var existingUser = await _db.Users
            .FirstOrDefaultAsync(u => u.Email.ToLower() == lowercaseEmail || u.PhoneNumber == user.PhoneNumber);

        if (existingUser != null)
        {
            throw new Exception("The email address and/or the phone number already exists in the database.");
        }

        await _db.Users.AddAsync(user);
        await _db.SaveChangesAsync();
        return user;
    }

    public async Task<User?> DeleteUserAsync(int id)
    {
        var user = await _db.Users.FindAsync(id);
        if (user != null)
        {
            _db.Users.Remove(user);
            await _db.SaveChangesAsync();
        }
        return user;
    }

    public async Task<User?> UpdateUserAsync(int id, User userToUpdate)
    {
        var user = await _db.Users.FindAsync(id);
        if (user != null)
        {
            user.FirstName = userToUpdate.FirstName;
            user.LastName = userToUpdate.LastName;
            user.Email = userToUpdate.Email;
            user.PhoneNumber = userToUpdate.PhoneNumber;
            user.Password = userToUpdate.Password;
            _db.Users.Update(user);
            await _db.SaveChangesAsync();
        }
        return user;
    }
}