using Microsoft.Extensions.Logging;
using Workspace.DAL;
using Workspace.Entities;
using Workspace.Entities.Contracts;

namespace Workspace.BLL.Logic.Services.PermissionService;

public class PermissionService(IUserRepository userRepository, ILogger<PermissionService> logger) : IPermissionService
{
    private readonly IUserRepository _userRepository= userRepository;
    private readonly ILogger _logger = logger;

    public async Task<HashSet<Permission>> GetPermissionsAsync(Guid userId)
    {
        try
        {
            return await userRepository.GetUserPermission(userId);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка в ");
            throw;
        }
    }
}