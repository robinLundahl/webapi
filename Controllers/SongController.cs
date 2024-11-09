using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webapi.Service;
using webapi.infrastuctor;
using webapi.models;

namespace webapi.Controllers;

[ApiController]
[Route("/api")]
public class SongController : ControllerBase
{

    private readonly TrubadurenContext _db;
    private readonly SongService _songService;

    public SongController(TrubadurenContext db, SongService songService)
    {
        _db = db;
        _songService = songService;
    }

    [HttpGet("song")]
    public async Task<ActionResult> GetAllSongsAsync()
    {
        var songs = await _db.Songs
        .OrderBy(s => s.ArtistName).ToListAsync();

        return Ok(songs);
    }

    [HttpGet("song/{id:int}")]
    public async Task<ActionResult> GetSongByIdAsync(int id)
    {
        var song = await _db.Songs.FindAsync(id);

        if (song == null)
        {
            return NotFound(new { message = "Song not found", id = song.Id });
        }

        return Ok(song);
    }

    [HttpPost("song")]
    public async Task<ActionResult> AddSongAsync(Song song)
    {
        try
        {
            var songAdded = await _songService.AddSongAsync(song);

            return Created("Song" + songAdded.SongName, songAdded);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpDelete("song/{id:int}")]
    public async Task<ActionResult> DeleteSongAsync(int id)
    {
        var deletedSong = await _songService.DeleteSongAsync(id);
        if (deletedSong != null)
        {
            return NoContent();
        }
        else
        {
            return BadRequest("You can't delete a song that doesn't exist.");
        }
    }

    [HttpPut("song/{id:int}")]
    public async Task<ActionResult> UpdateSongAsync(int id, Song songToUpdate)
    {
        var updatedSong = await _songService.UpdateSongAsync(id, songToUpdate);
        if (updatedSong != null)
        {
            return Ok("The song was successfully updated.");
        }
        else
        {
            return BadRequest("Not possible to update the song.");
        }
    }
}