using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webapi.Service;
using webapi.Infrastructor;
using Entities.Models;

namespace webapi.Controllers;

[ApiController]
[Route("/api")]
public class SongController : ControllerBase
{
    private readonly SongService _songService;

    public SongController(SongService songService)
    {
        _songService = songService;
    }

    [HttpGet("song")]
    public async Task<ActionResult> GetAllSongsAsync()
    {
        try
        {
            return Ok(await _songService.GetAllSongsAsync());
        }
        catch
        {
            return NotFound("Could not find the requested list of songs.");
        }
    }

    [HttpGet("song/{id:int}")]
    public async Task<ActionResult> GetSongByIdAsync(int id)
    {
        try
        {
            return Ok(await _songService.GetSongByIdAsync(id));
        }
        catch
        {
            return NotFound("Could not find the requested song.");
        }
    }

    [HttpPost("song")]
    public async Task<ActionResult> AddSongAsync(Song song)
    {
        try
        {
            await _songService.AddSongAsync(song);
            return Created();
        }
        catch
        {
            return BadRequest("Failed to add new song.");
        }
    }

    [HttpDelete("song/{id:int}")]
    public async Task<ActionResult> DeleteSongAsync(int id)
    {
        try
        {
            var deletedSong = await _songService.DeleteSongAsync(id);
            if (deletedSong != null)
            {
                return NoContent();
            }

            return BadRequest("You can't delete a song that doesn't exist.");
        }
        catch
        {
            return StatusCode(500, "An error occurred while deleting the song.");
        }
    }

    [HttpPut("song/{id:int}")]
    public async Task<ActionResult> UpdateSongAsync(int id, Song songToUpdate)
    {
        try
        {
            var updatedSong = await _songService.UpdateSongAsync(id, songToUpdate);
            if (updatedSong != null)
            {
                return Ok("The song was successfully updated.");
            }

            return BadRequest("Not possible to update the song.");
        }
        catch
        {
            return StatusCode(500, "An error occurred while updating the song.");
        }
    }
}