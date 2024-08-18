using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


using Workspace.BLL.Logic.Contracts;
using Workspace.Entities;

namespace Workspace.PL;

[Route("api/[controller]")]
[ApiController]

public class WorkspaceTaskController(ITaskService taskService, ILogger<WorkspaceTaskController> logger) : ControllerBase
{
    private readonly ITaskService _taskService = taskService;
    private readonly ILogger _logger = logger;

    /// <summary>
    /// Запрос всех задач
    /// </summary>
    [Authorize]
    [HasPermission([Permission.user, Permission.read])]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<WorkspaceTaskResponse>>> GetAsync()
    {
        try
        {
            var _workspaceTasksResponse = await _taskService.GetAllAsync();
            return Ok(_workspaceTasksResponse);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка в GetAllAsync");
            throw;
        }
    }

    /// <summary>
    /// Запрос задач по ID
    /// </summary>
    [Authorize]
    [HasPermission([Permission.user, Permission.read])]
    [HttpGet("{id}")]
    public async Task<ActionResult<WorkspaceTaskResponse>> GetAsync(Guid id)
    {
        try
        {
            var _workspaceTaskResponse = await _taskService.GetByIdAsync(id);
     
            if (_workspaceTaskResponse is null)
                return NotFound();

            return Ok(_workspaceTaskResponse);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка в GetAsync по id");
            throw;
        }
    }

    /// <summary>
    /// Изменение задачи
    /// </summary>
    [Authorize]
    [HasPermission([Permission.user, Permission.update])]
    [HttpPut("{id}")]
    public async Task UpdateAsync(Guid id, [FromBody] WorkspaceTaskRequest workspaceTaskRequest)
    {
        try
        {
            var validator = new TaskValidator();
            var validationResult = validator.Validate(workspaceTaskRequest);

            if (!validationResult.IsValid)
            {
                var error = string.Empty;

                foreach (var item in validationResult.Errors)
                {
                    error += $"{item} \n";
                }

                throw new Exception(error);
            }

            await _taskService.UpdateAsync(id, workspaceTaskRequest);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка в UpdateAsync");
            throw;
        }
    }


    /// <summary>
    /// Создание задачи
    /// </summary>
    [Authorize]
    [HasPermission([Permission.user, Permission.create])]
    [HttpPost]
    public async Task CreateAsync([FromBody] WorkspaceTaskRequest workspaceTaskRequest)
    {
        try
        {
            workspaceTaskRequest.Status = workspaceTaskRequest.Status < 1 ? 1 : workspaceTaskRequest.Status;
            var validator = new TaskValidator();
            var validationResult = validator.Validate(workspaceTaskRequest);

            if (!validationResult.IsValid)
            {
                var error = string.Empty;

                foreach (var item in validationResult.Errors)
                {
                    error += $"{item} \n";
                }

                throw new Exception(error);
            }

            await _taskService.CreateAsync(workspaceTaskRequest);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка в CreateAsync");
            throw;
        }
    }

    /// <summary>
    /// Удаление задачи
    /// </summary>
    [Authorize]
    [HasPermission([Permission.user, Permission.delete])]
    [HttpDelete("{id}")]
    public async Task DeleteAsync(Guid id)
    {
        try
        {
            await _taskService.DeleteAsync(id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка в DeleteAsync");
            throw;
        }
    }

    /// <summary>
    /// Запрос задач по ид марта
    /// </summary>
    /// <remarks>
    /// Возвращает таски которые созданы для конкретного марта, при этом за тасками
    /// не закреплен еще исполнитель и статус таска "НОВЫЙ"
    /// </remarks>
    /// <param name="martId"></param>
    /// <returns></returns>
    [Authorize]
    [HasPermission([Permission.user, Permission.read])]
    [HttpGet("workspacemart/{martId}")]
    public async Task<ActionResult<WorkspaceTaskResponse>> GetAllTasksForMartAsync(Guid martId)
    {
        try
        {
            var _workspaceTaskResponse = await _taskService.GetAllTasksForMartAsync(martId);

            return Ok(_workspaceTaskResponse);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка в GetAsync по id");
            throw;
        }
    }

    /// <summary>
    /// Назначение пользователя за таском
    /// </summary>
    /// <param name="id">Ид таска</param>
    /// <param name="workspaceTaskRequest">Передаем только ид пользователя</param>
    /// <returns></returns>
    [HttpPut("workspacemart/{id}")]
    public async Task SetEmployeeAsync(Guid id, [FromBody] WorkspaceTaskRequest workspaceTaskRequest)
    {
        try
        {
            await _taskService.SetEmployeeAsync(id, workspaceTaskRequest);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка в назначении сотрудника за таском");
            throw;
        }
    }
}