using UsersTasksBackend.DTOs;

namespace UsersTasksBackend.Services.Interfaces;

public interface IUsersService
{
    Task<IEnumerable<UserDto>> GetAll(bool? hasTasks);
    Task<UserDto> Create(CreateUserDto dto);
    Task<bool> Update(int id, UpdateUserDto dto);
    Task<bool> Delete(int id);
}