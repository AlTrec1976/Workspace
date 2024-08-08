using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

using Workspace.Entities;

namespace Workspace.DAL;

public class TaskRepository : BaseRepository, ITaskRepository
{
    private readonly ILogger _logger;

    //TODO: ���������� �� ���������������� ��������� �����
    public TaskRepository(ILogger<TaskRepository> logger, IConfiguration configuration)
        : base(logger, configuration)
    {
        _logger = logger;
    }

    public async Task<IEnumerable<WorkspaceTaskDTO>> GetAllTasksAsync()
    {
        var sql = "SELECT * FROM public.get_all_tasks()";

        try
        {
            return await QueryAsync<WorkspaceTaskDTO>(sql);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "������ ��� ������ ������ �������");
            throw;
        }
    }

    public async Task<WorkspaceTaskDTO> GetByIDAsync(Guid taskId)
    {
        var sql = "SELECT * FROM public.get_task(@id)";

        try
        {
            var param = new { id = taskId };
            return await QuerySingleAsync<WorkspaceTaskDTO>(sql, param);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "������ ��� ������ ������� �� Id");
            throw;
        }
    }

    public async Task UpdateAsync(WorkspaceTaskDTO workspaceTaskDTO)
    {
        var sql = "CALL public.update_task(@id, @name, @status, @note, @managerid, @employeeid)";

        try
        {
            var param = new
            {
                id = workspaceTaskDTO.Id,
                name = workspaceTaskDTO.Name,
                status = workspaceTaskDTO.Status,
                note = workspaceTaskDTO.Notes,
                managerid = workspaceTaskDTO.ManagerId,
                employeeid = workspaceTaskDTO.EmployeeId
            };

            await ExecuteAsync(sql, param);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "������ ��� ���������� �������");
            throw;
        }
    }

    public async Task CreateAsync(WorkspaceTaskDTO workspaceTaskDTO)
    {
        var sql = "CALL public.create_task(@name, @status, @note, @managerid, @employeeid)";

        try
        {
            var param = new
            {
                name = workspaceTaskDTO.Name,
                status = workspaceTaskDTO.Status,
                note = workspaceTaskDTO.Notes,
                managerid = workspaceTaskDTO.ManagerId,
                employeeid = workspaceTaskDTO.EmployeeId
            };

            await ExecuteAsync(sql, param);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "������ ��� �������� �������");
            throw;
        }
    }

    public async Task DeleteAsync(Guid taskId)
    {
        var sql = "CALL public.delete_task(@id)";

        try
        {
            var param = new { id = taskId };
            await ExecuteAsync(sql, param);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "������ ��� �������� �������");
            throw;
        }
    }
}
