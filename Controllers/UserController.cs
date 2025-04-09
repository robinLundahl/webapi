using Microsoft.AspNetCore.Mvc;
using Entities.Models;
using Entitites.DTOs.User;
using webapi.Service.UserInterface;

namespace webapi.Controllers;

[ApiController]
[Route("/api")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet("user")]
    public async Task<ActionResult<IEnumerable<UserDTO>>> GetAllUsersAsync()
    {
        try
        {
            return Ok(await _userService.GetAsync());
        }
        catch
        {
            return NotFound("Could not find the requested list of users.");
        }
    }

    [HttpGet("user/{id:Guid}")]
    [ActionName("GetUserByIdAsync")]
    public async Task<ActionResult<UserDTO>> GetUserByIdAsync(Guid id)
    {
        try
        {
            var user = await _userService.GetAsync(id);
            if (user == null)
            {
                return NotFound($"User with ID {id} was not found");
            }
            return Ok(user);
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
            var createdUser = await _userService.CreateAsync(user);

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

    [HttpDelete("user/{id:Guid}")]
    public async Task<ActionResult> DeleteUserAsync(User user)
    {
        try
        {
            var deletedUser = await _userService.DeleteAsync(user);
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

    [HttpPut("user/{id:Guid}")]
    public async Task<ActionResult> UpdateUserAsync(Guid id, User userToUpdate)
    {
        try
        {
            var updatedUser = await _userService.UpdateAsync(id, userToUpdate);
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