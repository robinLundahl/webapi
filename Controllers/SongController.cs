using Microsoft.AspNetCore.Mvc;
using Entities.Models;
using webapi.Services.SongInterface;

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

    [HttpGet("songs")]
    public async Task<ActionResult> GetAllSongsAsync()
    {
        try
        {
            return Ok(await _songService.GetAsync());
        }
        catch
        {
            return NotFound("Could not find the requested list of songs.");
        }
    }

    [HttpGet("song/{id:Guid}")]
    [ActionName("GetSongByIdAsync")]
    public async Task<ActionResult> GetSongByIdAsync(Guid id)
    {
        try
        {
            return Ok(await _songService.GetAsync(id));
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
            var createdSong = await _songService.CreateAsync(song);

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

    [HttpDelete("song/{id:Guid}")]
    public async Task<ActionResult> DeleteSongAsync(Guid id)
    {
        try
        {
            var deletedSong = await _songService.DeleteAsync(id);
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

    [HttpPut("song/{id:Guid}")]
    public async Task<ActionResult> UpdateSongAsync(Guid id, Song songToUpdate)
    {
        try
        {
            var updatedSong = await _songService.UpdateAsync(id, songToUpdate);
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