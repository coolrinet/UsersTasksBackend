using System.ComponentModel.DataAnnotations;
using TaskStatus = UsersTasksBackend.Models.Enums.TaskStatus;

namespace UsersTasksBackend.DTOs;

public class TaskDto
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;
    
    public TaskStatus Status { get; set; }
    
    public UserDto? User { get; set; }
}