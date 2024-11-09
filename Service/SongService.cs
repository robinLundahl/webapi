using Microsoft.EntityFrameworkCore;
using webapi.infrastuctor;
using webapi.models;

namespace webapi.Service;

public class SongService
{
    private readonly TrubadurenContext _db;

    public SongService(TrubadurenContext db)
    {
        _db = db;
    }

    public async Task<Song?> GetSongByIdAsync(int id)
    {
        var song = await _db.Songs.FirstOrDefaultAsync(s => s.Id == id);
        return song;
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


