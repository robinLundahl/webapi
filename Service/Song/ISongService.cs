using Entities.Models;

namespace webapi.Service.SongInterface;

public interface ISongService
{
    Task<Song?> GetSongByIdAsync(int id);
    Task<List<Song>> GetAllSongsAsync();
    Task<Song?> AddSongAsync(Song song);
    Task<Song?> DeleteSongAsync(int id);
    Task<Song?> UpdateSongAsync(int id, Song songToUpdate);
}