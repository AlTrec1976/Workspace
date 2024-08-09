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
    /// ������ ���� �����
    /// </summary>
    [Authorize]
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
            _logger.LogError(ex, "������ � GetAllAsync");
            throw;
        }
    }

    /// <summary>
    /// ������ ����� �� ID
    /// </summary>
    [Authorize]
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
            _logger.LogError(ex, "������ � GetAsync �� id");
            throw;
        }
    }

    /// <summary>
    /// ��������� ������
    /// </summary>
    [Authorize]
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
            _logger.LogError(ex, "������ � UpdateAsync");
            throw;
        }
    }


    /// <summary>
    /// �������� ������
    /// </summary>
    [Authorize]
    [HttpPost]
    public async Task CreateAsync([FromBody] WorkspaceTaskRequest workspaceTaskRequest)
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

            await _taskService.CreateAsync(workspaceTaskRequest);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "������ � CreateAsync");
            throw;
        }
    }

    /// <summary>
    /// �������� ������
    /// </summary>
    [Authorize]
    [HttpDelete("{id}")]
    public async Task DeleteAsync(Guid id)
    {
        try
        {
            await _taskService.DeleteAsync(id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "������ � DeleteAsync");
            throw;
        }
    }
}