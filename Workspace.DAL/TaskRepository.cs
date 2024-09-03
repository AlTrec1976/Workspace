using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Data;
using System.Threading.Tasks;
using Workspace.Entities;

namespace Workspace.DAL;

public class TaskRepository(ILogger<TaskRepository> logger, IConfiguration configuration) 
    : BaseRepository(logger, configuration), ITaskRepository
{
    private readonly ILogger _logger = logger;

    public IAsyncEnumerable<WorkspaceTaskDTO> GetAllTasksAsync()
    {
        var sql = "SELECT * FROM public.get_all_tasks()";

        return Query<WorkspaceTaskDTO>(sql);
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

    public bool IsTaskOwner(WorkspaceTaskDTO task, WorkspaceUserDTO user)
    {
        using var connection = GetConnection();

        var sql = "public.is_manager";

        var param = new DynamicParameters();
        param.Add("@task_id", task.Id);
        param.Add("@user_id", user.Id);
        param.Add("@is_man", dbType: DbType.Boolean, direction: ParameterDirection.Output);
        
        connection.Execute(sql, param, commandType: CommandType.StoredProcedure);

        var isManager = param.Get<Boolean>("@is_man");
        return isManager;
    }

    public async Task UpdateStatus(WorkspaceTaskDTO workspaceTaskDTO)
    {
        using var connection = GetConnection();

        var sql = "public.update_task_status";

        var param = new DynamicParameters();
        param.Add("@taskid", workspaceTaskDTO.Id);
        param.Add("@tstatus", workspaceTaskDTO.Status);
        
        await connection.ExecuteAsync(sql, param, commandType: CommandType.StoredProcedure);
    }
}
