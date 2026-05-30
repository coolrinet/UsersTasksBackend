using Microsoft.EntityFrameworkCore;
using UsersTasksBackend.Context;
using UsersTasksBackend.DTOs;
using UsersTasksBackend.Services.Interfaces;
using Task = UsersTasksBackend.Models.Task;

namespace UsersTasksBackend.Services.Implementations;

public class TasksService : ITasksService
{
    private readonly UsersTasksContext _context;
    
    public TasksService(UsersTasksContext context)
    {
        _context = context;
    }
    
    public async Task<IEnumerable<TaskDto>> GetAll(bool? withUsers)
    {
        var query = withUsers switch
        {
            true => _context.Tasks.Join(_context.Users, t => t.UserId, u => u.Id,
                (t, u) => new TaskDto
                {
                    Id = t.Id,
                    Title = t.Title,
                    Description = t.Description,
                    Status = t.Status,
                    User = new UserDto { Id = u.Id, Name = u.Name, Email = u.Email, }
                }),
            false => _context.Tasks.Where(t => t.UserId == null)
                .Select(t => new TaskDto
                {
                    Id = t.Id, Title = t.Title, Description = t.Description, Status = t.Status,
                }),
            _ => _context.Tasks.Select(t => new TaskDto
            {
                Id = t.Id,
                Title = t.Title,
                Description = t.Description,
                Status = t.Status,
                User = t.User == null ? null : new UserDto { Id = t.User.Id, Name = t.User.Name, Email =  t.User.Email }
            })
        };

        var tasks = await query.OrderBy(t => t.Id).ToListAsync();
        
        return tasks;
    }

    public async Task<TaskDto> Create(CreateTaskDto dto)
    {
        var taskEntity = (await  _context.Tasks.AddAsync(new Task
        {
            Title = dto.Title,
            Description = dto.Description,
            Status = dto.Status,
            UserId = dto.UserId,
        })).Entity;
        
        await _context.SaveChangesAsync();

        return new TaskDto
        {
            Id = taskEntity.Id,
            Title = taskEntity.Title,
            Description = taskEntity.Description,
            Status = taskEntity.Status,
            User = taskEntity.User == null
                ? null
                : new UserDto
                {
                    Id = taskEntity.User.Id,
                    Name = taskEntity.User.Name,
                    Email = taskEntity.User.Email,
                }
        };
    }

    public async Task<bool> Update(int id, UpdateTaskDto dto)
    {
        var task = await _context.Tasks.FindAsync(id);

        if (task == null)
        {
            return false;
        }
        
        task.Title = dto.Title;
        task.Description = dto.Description;
        task.Status = dto.Status;
        task.UserId = dto.UserId;
        await _context.SaveChangesAsync();
        
        return true;
    }

    public async Task<bool> Delete(int id)
    {
        var task = await _context.Tasks.FindAsync(id);

        if (task == null)
        {
            return false;
        }
        
        _context.Tasks.Remove(task);
        await  _context.SaveChangesAsync();
        
        return true;
    }
}