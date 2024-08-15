using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Data;

using Workspace.Entities;

namespace Workspace.DAL
{
    public class MartRepository : BaseRepository, IMartRepository
    {
        private readonly ILogger _logger;

        public MartRepository(ILogger<MartRepository> logger, IConfiguration configuration)
            : base(logger, configuration)
        {
            _logger = logger;
        }

        public async Task <WorkspaceMartDTO> CreateMartAsync(WorkspaceMartDTO workspaceMartDTO)
        {
            using var connection = GetConnection();

            var sql = "public.create_workspacemart";

            var param = new DynamicParameters();
            param.Add("@name_w", workspaceMartDTO.Name);
            param.Add("@owner_id", workspaceMartDTO.OwnerId);
            param.Add("@mart_id", dbType: DbType.Guid, direction: ParameterDirection.Output);

            await connection.ExecuteAsync(sql, param, commandType: CommandType.StoredProcedure);

            workspaceMartDTO.Id = param.Get<Guid>("@mart_id");

            return workspaceMartDTO;
        }

        public async Task<WorkspaceTaskDTO> CreateTaskAsync(WorkspaceMartDTO workspaceMartDTO,
                                                            WorkspaceTaskDTO workspaceTaskDTO)
        {
            using var connection = GetConnection();

            var sql = "public.create_task_mart";
            var param = new DynamicParameters();
            param.Add("@mart_id", workspaceMartDTO.Id);
            param.Add("@task_name", workspaceTaskDTO.Name);
            param.Add("@task_status", 1);
            param.Add("@task_id", dbType: DbType.Guid, 
                                  direction: ParameterDirection.Output);
            param.Add("@owner_id", dbType: DbType.Guid,
                                  direction: ParameterDirection.Output);
            
            await connection.ExecuteAsync(sql, param, commandType: CommandType.StoredProcedure);
            
            workspaceTaskDTO.Id = param.Get<Guid>("@task_id");
            workspaceTaskDTO.ManagerId = param.Get<Guid>("@owner_id");
            return workspaceTaskDTO;
        }

        public async Task<WorkspaceMart> GetMartAsync(Guid martid)
        {
            var sql = "select * from public.get_mart_tasks(wmart_id)";
            var param = new DynamicParameters();
            param.Add("@wmart_id", martid);
            
            var workspaceMart = await QuerySingleAsync<WorkspaceMart>(sql, param);
            
            return workspaceMart;
        }
    }
}