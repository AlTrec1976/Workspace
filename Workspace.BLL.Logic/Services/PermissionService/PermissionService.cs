using Workspace.DAL;
using Workspace.Entities;
using Workspace.Entities.Contracts;

namespace Workspace.BLL.Logic.Services.PermissionService;

public class PermissionService(IUserRepository userRepository) : IPermissionService
{
    private readonly IUserRepository _userRepository= userRepository;
    public async Task<HashSet<Permission>> GetPermissionsAsync(Guid userId)
    {
        return await userRepository.GetUserPermission(userId);
    }
}