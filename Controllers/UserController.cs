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
        var users = await _db.Users
        .OrderBy(s => s.LastName).ToListAsync();

        return Ok(users);
    }

    [HttpGet("user/{id:int}")]
    public async Task<ActionResult> GetUserByIdAsync(int id)
    {
        var user = await _db.Users.FindAsync(id);

        if (user == null)
        {
            return NotFound(new { message = "user not found", id = user.Id });
        }

        return Ok(user);
    }

    [HttpPost("user")]
    public async Task<ActionResult> AdduserAsync(User user)
    {
        try
        {
            var userAdded = await _userService.AddUserAsync(user);

            return Created("user" + userAdded.FirstName, userAdded);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpDelete("user/{id:int}")]
    public async Task<ActionResult> DeleteuserAsync(int id)
    {
        var deletedUser = await _userService.DeleteUserAsync(id);
        if (deletedUser != null)
        {
            return NoContent();
        }
        else
        {
            return BadRequest("You can't delete a user that doesn't exist.");
        }
    }

    [HttpPut("user/{id:int}")]
    public async Task<ActionResult> UpdateuserAsync(int id, User userToUpdate)
    {
        var updatedUser = await _userService.UpdateUserAsync(id, userToUpdate);
        if (updatedUser != null)
        {
            return Ok("The user was successfully updated.");
        }
        else
        {
            return BadRequest("Not possible to update the user.");
        }
    }
}