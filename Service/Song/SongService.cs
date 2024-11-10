using Microsoft.EntityFrameworkCore;
using webapi.Infrastructor;
using Entities.Models;
using webapi.Service.SongInterface;

namespace webapi.Service;

public class SongService : ISongService
{
    private readonly TrubadurenContext _db;
    private readonly ILogger<SongService> _logger;

    public SongService(TrubadurenContext db, ILogger<SongService> logger)
    {
        _logger = logger;
        _db = db;
    }

    public async Task<Song?> GetSongByIdAsync(int id)
    {
        try
        {
            return await _db.Songs.FirstOrDefaultAsync(s => s.Id == id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to retrieve the user with id: {id}.", id);
            throw;
        }
    }

    public async Task<List<Song>> GetAllSongsAsync()
    {
        try
        {
            return await _db.Songs
            .OrderBy(s => s.ArtistName).ToListAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to retrieve the songs.");
            throw;
        }
    }
    public async Task<Song?> AddSongAsync(Song song)
    {
        string lowercaseSongName = song.SongName.ToLower();
        string lowercaseArtistName = song.ArtistName.ToLower();

        var existingSong = await _db.Songs
            .FirstOrDefaultAsync(s => s.SongName.ToLower() == lowercaseSongName && s.ArtistName.ToLower() == lowercaseArtistName);

        if (existingSong != null)
        {
            throw new Exception("The song already exists in the database with the same song and artist.");
        }

        await _db.Songs.AddAsync(song);
        await _db.SaveChangesAsync();
        return song;
    }

    public async Task<Song?> DeleteSongAsync(int id)
    {
        var song = await _db.Songs.FindAsync(id);
        if (song != null)
        {
            _db.Songs.Remove(song);
            await _db.SaveChangesAsync();
        }
        return song;
    }

    public async Task<Song?> UpdateSongAsync(int id, Song songToUpdate)
    {
        var song = await _db.Songs.FindAsync(id);
        if (song != null)
        {
            song.SongName = songToUpdate.SongName;
            song.ArtistName = songToUpdate.ArtistName;
            song.Comment = songToUpdate.Comment;
            song.Genre = songToUpdate.Genre;
            _db.Songs.Update(song);
            await _db.SaveChangesAsync();
        }
        return song;
    }

}


