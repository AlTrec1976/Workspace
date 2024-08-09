using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

using Workspace.Entities;

namespace Workspace.DAL;

public class UserRepository : BaseRepository, IUserRepository
{
    private readonly ILogger _logger;

    public UserRepository(ILogger<UserRepository> logger, IConfiguration configuration)
        : base(logger, configuration)
    {
        _logger = logger;
    }
    
    public async Task<IEnumerable<WorkspaceUserDTO>> GetAllUsersAsync()
    {
        var sql = "SELECT * FROM public.get_all_users()";

        try
        {
            return await QueryAsync<WorkspaceUserDTO>(sql);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при выводе списка User");
            throw;
        }
    }

    public async Task<WorkspaceUserDTO> GetByIDAsync(Guid userId)
    {
        var sql = "SELECT * FROM public.get_user(@id)";

        try
        {
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
        var sql = "CALL public.update_user(@id, @login, @password, @name, @surname)";

        try
        {
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
        var sql = "CALL public.create_user(@login, @password, @name, @surname)";

        try
        {
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
        var sql = "CALL public.delete_user(@id)";

        try
        {
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
        var sql = @"SELECT * FROM public.get_permission(@id)";
        var permission = new Permission();
        
        var param = new { id = userID };

        var hst = new HashSet<Permission>(await QueryAsync<Permission>(sql, param));
        

        return hst;
    }
}
