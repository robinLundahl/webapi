using System.IO.Compression;
using Entities.Models;
using Entitites.DTOs.User;
using Mapster;
using Microsoft.EntityFrameworkCore;
using webapi.Infrastructor;

namespace webapi.Repositories;

public class UserRepository : IUserRepository
{
    private readonly TrubadurenContext _dbContext;
    public UserRepository(TrubadurenContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<UserDTO>> GetAsync()
    {
        var users = await _dbContext.Users.OrderBy(u => u.Id).ToListAsync();
        return users.Adapt<List<UserDTO>>();
    }

    public async Task<UserDTO?> GetAsync(Guid id)
    {
        var user = await _dbContext.Users.Where(x => x.Id == id).FirstOrDefaultAsync();
        return user.Adapt<UserDTO>();
    }

    public async Task<User> GetAsyncIncludePassword(Guid id)
    {
        var user = await _dbContext.Users.Where(x => x.Id == id).FirstOrDefaultAsync();
        return user;
    }

    public async Task<UserDTO> CreateAsync(User user)
    {
        await _dbContext.Users.AddAsync(user);
        await _dbContext.SaveChangesAsync();
        return user.Adapt<UserDTO>();
    }
    public async Task<UserDTO> UpdateAsync(User user)
    {
        _dbContext.Users.Update(user);
        await _dbContext.SaveChangesAsync();
        return user.Adapt<UserDTO>();
    }
    public async Task<User> DeleteAsync(User user)
    {
        _dbContext.Users.Remove(user);
        await _dbContext.SaveChangesAsync();
        return user;
    }
}