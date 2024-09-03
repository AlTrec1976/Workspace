using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Data;

using Workspace.Entities;

namespace Workspace.DAL;

public class UserRepository(ILogger<UserRepository> logger, IConfiguration configuration) 
    : BaseRepository(logger, configuration), IUserRepository
{
    private readonly ILogger _logger = logger;

    public IAsyncEnumerable<WorkspaceUserDTO> GetAllUsersAsync()
    {
        var sql = "SELECT * FROM public.get_all_users()";

        return Query<WorkspaceUserDTO>(sql);
    }

    public async Task<WorkspaceUserDTO> GetByIDAsync(Guid userId)
    {
        try
        {
            var sql = "SELECT * FROM public.get_user(@id)";

            var param = new { id = userId };
            return await QuerySingleAsync<WorkspaceUserDTO>(sql, param);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при поиске пользователя по Id");
            throw;
        }
    }

    public async Task UpdateAsync(WorkspaceUserDTO workspaceUserDTO)
    {
        try
        {
            var sql = "CALL public.update_user(@id, @login, @password, @name, @surname)";

            var param = new
            {
                id = workspaceUserDTO.Id,
                login = workspaceUserDTO.Login,
                password = workspaceUserDTO.Password,
                name = workspaceUserDTO.Name,
                surname = workspaceUserDTO.Surname,
            };

            await ExecuteAsync(sql, param);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при обновлении пользователя");
            throw;
        }
    }

    public async Task CreateAsync(WorkspaceUserDTO workspaceUserDTO)
    {
        try
        {
            var sql = "CALL public.create_user(@login, @password, @name, @surname)";

            var param = new
            {
                login = workspaceUserDTO.Login,
                password = workspaceUserDTO.Password,
                name = workspaceUserDTO.Name,
                surname = workspaceUserDTO.Surname,
            };

            await ExecuteAsync(sql, param);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при создании пользователя");
            throw;
        }
    }

    public async Task DeleteAsync(Guid userId)
    {
        try
        {
            var sql = "CALL public.delete_user(@id)";

            var param = new { id = userId };
            await ExecuteAsync(sql, param);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при удалении пользователя");
            throw;
        }
    }

    public async Task<WorkspaceUserDTO?> GetByUserLoginAsync(string userLogin)
    {
        try
        {
            var sql = "SELECT * FROM public.check_user_login(@login)";
            var param = new { login = userLogin };
            return await QuerySingleAsync<WorkspaceUserDTO>(sql, param);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при проверки логина пользователя");
            return new WorkspaceUserDTO();
        }
    }

    public async Task<HashSet<Permission>> GetUserPermission(Guid userID)
    {
        try
        {
            var sql = @"SELECT * FROM public.get_permission(@id)";
              
            var param = new { id = userID };

            var hst = new HashSet<Permission>(await QueryAsync<Permission>(sql, param));

            return hst;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка в GetUserPermission");
            throw;
        }
    }

    public async Task CreateUserRoleAsync(WorkspaceUserRoleDTO workspaceUserRoleDto)
    {
        try
        {
        var connection = GetConnection();
        var sql = "public.create_user_role";
        var param = new DynamicParameters();
        param.Add("@userid", workspaceUserRoleDto.Id);
        param.Add("@roleid", workspaceUserRoleDto.RoleId);

        await connection.ExecuteAsync(sql, param, commandType: CommandType.StoredProcedure);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка в CreateUserRoleAsync");
            throw;
        }
    }
    public async Task DeleteUserRoleAsync(WorkspaceUserRoleDTO workspaceUserRoleDto)
    {
        try
        {
            var connection = GetConnection();
            var sql = "public.delete_user_role";
            var param = new DynamicParameters();
            param.Add("@userid", workspaceUserRoleDto.Id);
            param.Add("@roleid", workspaceUserRoleDto.RoleId);

            await connection.ExecuteAsync(sql, param, commandType: CommandType.StoredProcedure);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка в DeleteUserRoleAsync");
            throw;
        }
    }

    public async Task<IEnumerable<RoleDTO>> GetUserRolesAsync(Guid id)
    {
        try
        {
            var sql = "SELECT * FROM public.get_user_roles(@userid)";
            var param = new { userid = id };

            return await QueryAsync<RoleDTO>(sql, param);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при выводе списка");
            throw;
        }
    }
}
