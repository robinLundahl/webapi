namespace webapi.Repositories;

using Entities.Models;
using Entitites.DTOs.User;

public interface ISongRepository
{
    public Task<List<Song>> GetAsync();
    public Task<Song?> GetAsync(Guid id);
    public Task<Song> CreateAsync(Song song);
    public Task<Song> UpdateAsync(Song song);
    public Task<Song> DeleteAsync(Song song);
}
