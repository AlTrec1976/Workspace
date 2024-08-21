using Workspace.Entities;
using Workspace.Entities.Contracts;

namespace Workspace.BLL.Logic.Contracts;

public interface IUserService
{
    Task<string> LoginAsync(WorkspaceUserRequest workspaceUserRequest);
    Task<List<WorkspaceUserResponse>> GetAllAsync();
    Task<WorkspaceUserResponse?> GetByIdAsync(Guid id);
    Task UpdateAsync(Guid id, WorkspaceUserRequest workspaceUserRequest);
    Task CreateAsync(WorkspaceUserRequest workspaceUserRequest);
    Task DeleteAsync(Guid id);
    void ChangeStatus(IWorkspaceUser user, IWorkspaceTask task);
    Task CreateUserRoleAsync(WorkspaceUserRoleRequest workspaceUserRoleRequest);
    Task DeleteUserRoleAsync(WorkspaceUserRoleRequest workspaceUserRoleRequest);
    Task<List<RoleResponse>> GetAllRolesAsync(Guid id);
}
