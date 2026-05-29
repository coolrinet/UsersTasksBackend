using Microsoft.EntityFrameworkCore;
using UsersTasksBackend.Context;
using UsersTasksBackend.DTOs;
using UsersTasksBackend.Models;
using UsersTasksBackend.Services.Interfaces;

namespace UsersTasksBackend.Services.Implementations;

public class UsersService : IUsersService
{
    private readonly UsersTasksContext _context;
    
    public UsersService(UsersTasksContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<UserDto>> GetAll(bool? hasTasks)
    {
        var query = _context.Users.AsQueryable();

        switch (hasTasks)
        {
            case true:
                query = query.Where(u => u.Tasks.Any());
                break;
            case false:
                query = query.Where(u => !u.Tasks.Any());
                break;
        }
        
        return await query 
            .Select(u => new UserDto
            {
                Id = u.Id,
                Name = u.Name,
                Email =  u.Email
            })
            .ToListAsync();
    }

    public async Task<UserDto?> GetById(int id)
    {
        var user = await _context.Users.FindAsync(id);

        if (user == null)
        {
            return null;
        }

        return new UserDto
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email
        };
    }

    public async Task<UserDto> Create(CreateUserDto dto)
    {
        var userEntity = (await _context.Users.AddAsync(new User
        {
            Name =  dto.Name,
            Email =  dto.Email
        })).Entity;
        await _context.SaveChangesAsync();

        return new UserDto
        {
            Id = userEntity.Id,
            Name = userEntity.Name,
            Email = userEntity.Email
        };
    }

    public async Task<bool> Delete(int id)
    {
        var user = await _context.Users.FindAsync(id);

        if (user == null)
        {
            return false;
        }
        
        _context.Users.Remove(user);
        await  _context.SaveChangesAsync();
        
        return true;
    }
}