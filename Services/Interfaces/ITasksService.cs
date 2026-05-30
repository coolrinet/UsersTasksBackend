using UsersTasksBackend.DTOs;

namespace UsersTasksBackend.Services.Interfaces;

public interface ITasksService
{
    Task<IEnumerable<TaskDto>> GetAll();
    Task<TaskDto> Create(CreateTaskDto dto);
    Task<bool> Update(int id, UpdateTaskDto dto);
    Task<bool> Delete(int id);
}