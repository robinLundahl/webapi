using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webapi.Service;
using webapi.Infrastructor;
using Entities.Models;

namespace webapi.Controllers;

[ApiController]
[Route("/api")]
public class UserController : ControllerBase
{

    private readonly TrubadurenContext _db;
    private readonly UserService _userService;

    public UserController(TrubadurenContext db, UserService userService)
    {
        _db = db;
        _userService = userService;
    }

    [HttpGet("user")]
    public async Task<ActionResult> GetAllUsersAsync()
    {
        try
        {
            return Ok(await _userService.GetAllUsersAsync());
        }
        catch
        {
            return NotFound("Could not find the requested list of users.");
        }
    }

    [HttpGet("user/{id:int}")]
    public async Task<ActionResult> GetUserByIdAsync(int id)
    {
        try
        {
            return Ok(await _userService.GetUserByIdAsync(id));
        }
        catch
        {
            return NotFound("Could not find the requested user.");
        }
    }

    [HttpPost("user")]
    public async Task<ActionResult> AdduserAsync(User user)
    {
        try
        {
            await _userService.AddUserAsync(user);
            return Created();
        }
        catch
        {
            return BadRequest("Failed to add new user.");
        }
    }

    [HttpDelete("user/{id:int}")]
    public async Task<ActionResult> DeleteUserAsync(int id)
    {
        try
        {
            var deletedUser = await _userService.DeleteUserAsync(id);
            if (deletedUser != null)
            {
                return NoContent();
            }

            return BadRequest("You can't delete a user that doesn't exist.");
        }
        catch
        {
            return StatusCode(500, "An error occurred while deleting the user.");
        }
    }

    [HttpPut("user/{id:int}")]
    public async Task<ActionResult> UpdateUserAsync(int id, User userToUpdate)
    {
        try
        {
            var updatedUser = await _userService.UpdateUserAsync(id, userToUpdate);
            if (updatedUser != null)
            {
                return Ok("The user was successfully updated.");
            }

            return BadRequest("Not possible to update the user.");
        }
        catch
        {
            return StatusCode(500, "An error occurred while updating the user.");
        }
    }
}