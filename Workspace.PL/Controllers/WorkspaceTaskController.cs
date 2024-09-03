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
    public IAsyncEnumerable<WorkspaceTaskResponse> GetAsync()
    {
        return _taskService.GetAllAsync();
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
    [HasPermission([Permission.manager, Permission.update])]
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
    [HasPermission([Permission.manager, Permission.create])]
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
    [HasPermission([Permission.manager, Permission.delete])]
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
    [HasPermission([Permission.manager, Permission.read])]
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
    /// <param name="workspaceTaskEmployee"></param>
    /// <returns></returns>
    [Authorize]
    [HasPermission([Permission.manager, Permission.update])]
    [HttpPut("workspacemart")]
    public async Task SetEmployeeAsync([FromBody] WorkspaceTaskEmployee workspaceTaskEmployee)
    {
        try
        {
            await _taskService.SetEmployeeAsync(workspaceTaskEmployee);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка в назначении сотрудника за таском");
            throw;
        }
    }
    /// <summary>
    /// Изменяет статус задания
    /// </summary>
    /// <remarks>
    /// Изменяет статус задания в зависимости от контекста пользователя
    /// Менеджер или Сотрудник.
    /// </remarks>
    /// <param name="taskUserRequest"></param>
    /// <returns></returns>
    [Authorize]
    [HasPermission([Permission.user, Permission.update])]
    [HttpPut("status")]
    public async Task UpdateStatus([FromBody] TaskUserRequest taskUserRequest)
    { 
        await _taskService.ChangeStatus(taskUserRequest);
    }
}