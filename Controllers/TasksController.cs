using Microsoft.AspNetCore.Mvc;
using UsersTasksBackend.DTOs;
using UsersTasksBackend.Services.Interfaces;

namespace UsersTasksBackend.Controllers;

[Route("[controller]")]
[ApiController]
public class TasksController : ControllerBase
{
    private readonly ITasksService _tasksService;
    
    public TasksController(ITasksService tasksService)
    {
        _tasksService = tasksService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TaskDto>>> GetTasks([FromQuery] bool? withUsers)
    {
        var tasks = await _tasksService.GetAll(withUsers);
        
        return Ok(tasks);
    }

    [HttpPost]
    public async Task<ActionResult<TaskDto>> CreateTask(CreateTaskDto dto)
    {
        var newTask = await _tasksService.Create(dto);
        
        return Created(string.Empty, newTask);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> UpdateTask(int id, UpdateTaskDto dto)
    {
        var isUpdated = await _tasksService.Update(id, dto);
        
        return isUpdated ? Ok() : NotFound();
    }
    
    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteTask(int id)
    {
        var isDeleted = await _tasksService.Delete(id);
            
        return isDeleted ? NoContent() : NotFound();
    }
}