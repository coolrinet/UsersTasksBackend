using System.ComponentModel.DataAnnotations;

namespace UsersTasksBackend.DTOs;

public class CreateUserDto
{
    [Required]
    public string Name { get; set; } = null!;
    
    [Required]
    [EmailAddress]
    public string Email { get; set; } = null!;
}