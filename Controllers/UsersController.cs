using System.Data;
using Microsoft.AspNetCore.Mvc;
using UsersTasksBackend.DTOs;
using UsersTasksBackend.Exceptions;
using UsersTasksBackend.Services.Interfaces;

namespace UsersTasksBackend.Controllers;

[Route("[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IUsersService _usersService;
        
    public UsersController(IUsersService usersService)
    {
        _usersService = usersService;
    }
        
    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers([FromQuery] bool? hasTasks)
    {
        var users = await _usersService.GetAll(hasTasks);

        return Ok(users);
    }

    [HttpPost]
    public async Task<ActionResult<UserDto>> CreateUser(CreateUserDto user)
    {
        try
        {
            var newUser = await _usersService.Create(user);

            return Created(string.Empty, newUser);
        }
        catch (DuplicateException e)
        {
            return Conflict(new
            {
                field = e.Field,
                message = e.Message
            });
        }
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> UpdateUser(int id, UpdateUserDto user)
    {
        var isUpdated = await _usersService.Update(id, user);

        return  isUpdated ? NoContent() : NotFound();
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteUser(int id)
    {
        var isDeleted = await _usersService.Delete(id);
            
        return isDeleted ? NoContent() : NotFound();
    }
}
