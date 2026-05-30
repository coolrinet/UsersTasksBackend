namespace UsersTasksBackend.Models;

public partial class Task
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;

    public TaskStatus Status { get; set; }

    public int? UserId { get; set; }

    public virtual User? User { get; set; }
}
