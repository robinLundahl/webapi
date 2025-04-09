using Entities.Models;
using Microsoft.EntityFrameworkCore;
using webapi.Infrastructor;

namespace webapi.Repositories;

public class SongRepository : ISongRepository
{
    private readonly TrubadurenContext _dbContext;
    public SongRepository(TrubadurenContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<Song>> GetAsync()
    {
        return await _dbContext.Songs.OrderBy(u => u.Id).ToListAsync();
    }

    public async Task<Song?> GetAsync(Guid id)
    {
        return await _dbContext.Songs.Where(x => x.Id == id).FirstOrDefaultAsync();
    }

    public async Task<Song> CreateAsync(Song song)
    {
        await _dbContext.Songs.AddAsync(song);
        await _dbContext.SaveChangesAsync();
        return song;
    }
    public async Task<Song> UpdateAsync(Song song)
    {
        _dbContext.Songs.Update(song);
        await _dbContext.SaveChangesAsync();
        return song;
    }
    public async Task<Song> DeleteAsync(Song song)
    {
        _dbContext.Songs.Remove(song);
        await _dbContext.SaveChangesAsync();
        return song;
    }
}