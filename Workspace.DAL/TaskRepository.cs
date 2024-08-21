using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Workspace.Entities;

namespace Workspace.DAL;

public class TaskRepository : BaseRepository, ITaskRepository
{
    private readonly ILogger _logger;

    public TaskRepository(ILogger<TaskRepository> logger, IConfiguration configuration)
        : base(logger, configuration)
    {
        _logger = logger;
    }

    public async Task<IEnumerable<WorkspaceTaskDTO>> GetAllTasksAsync()
    {
        try
        {
            var sql = "SELECT * FROM public.get_all_tasks()";

            return await QueryAsync<WorkspaceTaskDTO>(sql);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при выводе списка заданий");
            throw;
        }
    }

    public async Task<WorkspaceTaskDTO> GetByIDAsync(Guid taskId)
    {
        try
        {
            var sql = "SELECT * FROM public.get_task(@id)";

            var param = new { id = taskId };
            return await QuerySingleAsync<WorkspaceTaskDTO>(sql, param);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при поиска задания по Id");
            throw;
        }
    }

    public async Task UpdateAsync(WorkspaceTaskDTO workspaceTaskDTO)
    {
        try
        {
            var sql = "CALL public.update_task(@id, @name, @status, @note, @managerid, @employeeid)";

            var param = new
            {
                id = workspaceTaskDTO.Id,
                name = workspaceTaskDTO.Name,
                status = workspaceTaskDTO.Status,
                //note = workspaceTaskDTO.Notes,
                managerid = workspaceTaskDTO.ManagerId,
                employeeid = workspaceTaskDTO.EmployeeId
            };

            await ExecuteAsync(sql, param);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при обновлении задания");
            throw;
        }
    }

    public async Task CreateAsync(WorkspaceTaskDTO workspaceTaskDTO)
    {
        try
        {
            var sql = "CALL public.create_task(@name, @status, @managerid, @employeeid)";

            var param = new
            {
                name = workspaceTaskDTO.Name,
                status = workspaceTaskDTO.Status,
                managerid = workspaceTaskDTO.ManagerId,
                employeeid = workspaceTaskDTO.EmployeeId
            };

            await ExecuteAsync(sql, param);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при создании задания");
            throw;
        }
    }
    
    public async Task DeleteAsync(Guid taskId)
    {
        try
        {
            var sql = "CALL public.delete_task(@id)";

            var param = new { id = taskId };
            await ExecuteAsync(sql, param);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при удалении задания");
            throw;
        }
    }
    public async Task<IEnumerable<WorkspaceTaskDTO>> GetAllTasksForMartAsync(Guid martId)
    {
        try
        {
            var sql = "SELECT * FROM public.get_mart_tasks(@wmart_id)";
            var param = new { wmart_id = martId };

            return await QueryAsync<WorkspaceTaskDTO>(sql,param);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при выводе списка заданий для martId = " + martId.ToString());
            throw;
        }
    }

    public async Task SetEmployeeAsync(WorkspaceTaskDTO workspaceTaskDTO)
    {
        try
        {
            var sql = "CALL public.update_task_employee(@taskid, @temployee)";

            var param = new
            {
                taskid = workspaceTaskDTO.Id,
                temployee = workspaceTaskDTO.EmployeeId
            };

            await ExecuteAsync(sql, param);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при назначении сотрудника");
            throw;
        }
    }
}
