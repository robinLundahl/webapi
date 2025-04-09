using Entities.Models;
using webapi.Services.SongInterface;
using webapi.Repositories;

namespace webapi.Service;

public class SongService : ISongService
{
    private readonly ISongRepository _songRepository;
    private readonly ILogger<SongService> _logger;

    public SongService(ISongRepository songRepository, ILogger<SongService> logger)
    {
        _songRepository = songRepository;
        _logger = logger;
    }

    public async Task<List<Song>> GetAsync()
    {
        try
        {
            return await _songRepository.GetAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to retrieve the songs.");
            throw;
        }
    }

    public async Task<Song?> GetAsync(Guid id)
    {
        try
        {
            return await _songRepository.GetAsync(id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to retrieve the user with id: {id}.", id);
            throw;
        }
    }

    public async Task<Song?> CreateAsync(Song song)
    {
        if (string.IsNullOrWhiteSpace(song.SongName) || string.IsNullOrWhiteSpace(song.ArtistName))
            throw new ArgumentException("Song name and artist name are both required.");

        var allSongs = await _songRepository.GetAsync();

        var existingSong = allSongs.FirstOrDefault(s =>
            string.Equals(s.SongName, song.SongName, StringComparison.OrdinalIgnoreCase) &&
            string.Equals(s.ArtistName, song.ArtistName, StringComparison.OrdinalIgnoreCase));

        if (existingSong != null)
        {
            throw new Exception("The song already exists in the database with the same song and artist.");
        }

        await _songRepository.CreateAsync(song);

        return song;
    }

    public async Task<Song?> UpdateAsync(Guid id, Song songToUpdate)
    {
        var song = await _songRepository.GetAsync(id);

        if (song != null)
        {
            song.SongName = songToUpdate.SongName;
            song.ArtistName = songToUpdate.ArtistName;
            song.Comment = songToUpdate.Comment;
            song.Genre = songToUpdate.Genre;
            await _songRepository.UpdateAsync(song);
        }

        return song;
    }

    public async Task<Song?> DeleteAsync(Guid id)
    {
        var song = await _songRepository.GetAsync(id);
        if (song != null)
        {
            await _songRepository.DeleteAsync(song);
        }
        return song;
    }
}