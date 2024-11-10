using Microsoft.AspNetCore.Mvc;
using Entities.Models;
using webapi.Service.SongInterface;

namespace webapi.Controllers;

[ApiController]
[Route("/api")]
public class SongController : ControllerBase
{
    private readonly ISongService _songService;

    public SongController(ISongService songService)
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
    [ActionName("GetSongByIdAsync")]
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
            var createdSong = await _songService.AddSongAsync(song);

            if (createdSong == null)
            {
                return BadRequest("Failed to add new song.");
            }
            return CreatedAtAction(
                nameof(GetSongByIdAsync),
                new { id = createdSong.Id },
                createdSong
            );
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
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