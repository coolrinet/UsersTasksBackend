namespace UsersTasksBackend.Exceptions;

public class DuplicateException(string field, string message) : Exception(message)
{
    public string Field { get; } = field;
}