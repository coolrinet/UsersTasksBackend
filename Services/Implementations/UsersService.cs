using Microsoft.EntityFrameworkCore;
using UsersTasksBackend.Context;
using UsersTasksBackend.DTOs;
using UsersTasksBackend.Exceptions;
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
        var query = hasTasks switch
        {
            true => _context.Users.Where(u => u.Tasks.Any())
                .Select(u => new UserDto
                {
                    Id = u.Id,
                    Name = u.Name,
                    Email = u.Email
                }),
            false => _context.Users.LeftJoin(
                    _context.Tasks,
                    u => u.Id,
                    t => t.UserId,
                    (u, t) => new {u, t})
                .Where(record => record.t == null)
                .Select(record => new UserDto
                {
                    Id = record.u.Id,
                    Name = record.u.Name,
                    Email = record.u.Email
                
                }),
            _ => _context.Users.Select(u => new UserDto
            {
                Id = u.Id,
                Name = u.Name,
                Email = u.Email
                
            })
        };

        var users = await query.OrderBy(u => u.Id).ToListAsync();
        
        return users;
    }

    public async Task<UserDto> Create(CreateUserDto dto)
    {
        if (await _context.Users.AnyAsync(u => u.Email == dto.Email))
        {
            throw new DuplicateException(nameof(CreateUserDto.Email), "Пользователь с данным email уже существует");
        }
        
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

    public async Task<bool> Update(int id, UpdateUserDto dto)
    {
        var user = await _context.Users.FindAsync(id);

        if (user == null)
        {
            return false;
        }
        
        user.Name = dto.Name;
        user.Email = dto.Email;
        await _context.SaveChangesAsync();
        
        return true;
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