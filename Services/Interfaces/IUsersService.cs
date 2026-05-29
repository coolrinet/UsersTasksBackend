using UsersTasksBackend.DTOs;
using UsersTasksBackend.Models;

namespace UsersTasksBackend.Services.Interfaces;

public interface IUsersService
{
    Task<IEnumerable<UserDto>> GetAll(bool? hasTasks);
    Task<UserDto?> GetById(int id);
    Task<UserDto> Create(CreateUserDto dto);
    Task<bool> Update(int id, UpdateUserDto dto);
    Task<bool> Delete(int id);
}