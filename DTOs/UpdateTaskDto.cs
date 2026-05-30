using System.ComponentModel.DataAnnotations;
using TaskStatus = UsersTasksBackend.Models.Enums.TaskStatus;

namespace UsersTasksBackend.DTOs;

public class UpdateTaskDto
{
    [Required]
    public string Title { get; set; } = null!;

    [Required]
    public string Description { get; set; } = null!;

    [Required]
    [EnumDataType(typeof(TaskStatus))]
    public TaskStatus Status { get; set; } = TaskStatus.Todo;
    
    public int? UserId { get; set; }
}