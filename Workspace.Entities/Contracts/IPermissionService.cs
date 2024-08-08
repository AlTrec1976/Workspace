using Workspace.Entities;

namespace Workspace.Entities.Contracts;

public interface IPermissionService
{
    Task<HashSet<Permission>> GetPermissionsAsync(Guid userId);
}