using Microsoft.AspNetCore.Mvc;
using UsersTasksBackend.DTOs;
using UsersTasksBackend.Models;
using UsersTasksBackend.Services.Interfaces;

namespace UsersTasksBackend.Controllers
{
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

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _usersService.GetById(id);

            if (user == null)
            {
                return NotFound();
            }
            
            return Ok(user);
        }

        [HttpPost]
        public async Task<ActionResult<User>> CreateUser(CreateUserDto user)
        {
            var newUser = await _usersService.Create(user);

            return CreatedAtAction(nameof(GetUser), new { id = newUser.Id }, newUser);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateUser(int id, UpdateUserDto user)
        {
            var isUpdated = await _usersService.Update(id, user);

            if (!isUpdated)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUser(int id)
        {
            var isDeleted = await _usersService.Delete(id);
            
            if (!isDeleted)
                return NotFound();
            
            return NoContent();
        }
    }
}
