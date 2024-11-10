using Microsoft.AspNetCore.Mvc;
using webapi.Service;
using Entities.Models;
using Entitites.DTOs.User;

namespace webapi.Controllers;

[ApiController]
[Route("/api")]
public class UserController : ControllerBase
{
    private readonly UserService _userService;

    public UserController(UserService userService)
    {
        _userService = userService;
    }

    [HttpGet("user")]
    public async Task<ActionResult<IEnumerable<UserDTO>>> GetAllUsersAsync()
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
    [ActionName("GetUserByIdAsync")]
    public async Task<ActionResult<UserDTO>> GetUserByIdAsync(int id)
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
    public async Task<ActionResult<UserDTO>> AddUserAsync(User user)
    {
        try
        {
            Console.WriteLine("Denna användare har följande information:" + user.Id);
            var createdUser = await _userService.AddUserAsync(user);

            if (createdUser == null)
            {
                return BadRequest("Failed to add new user.");
            }
            return CreatedAtAction(
                nameof(GetUserByIdAsync),
                new { id = createdUser.Id },
                createdUser
            );
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
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