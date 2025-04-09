using Entities.Models;

namespace webapi.Services.SongInterface;

public interface ISongService
{
    Task<Song?> GetAsync(Guid id);
    Task<List<Song>> GetAsync();
    Task<Song?> CreateAsync(Song song);
    Task<Song?> UpdateAsync(Guid id, Song songToUpdate);
    Task<Song?> DeleteAsync(Guid id);
}