using Workspace.Entities;

namespace Workspace.DAL;
public interface IUserRepository
{
    IAsyncEnumerable<WorkspaceUserDTO> GetAllUsersAsync();
    Task<WorkspaceUserDTO> GetByIDAsync(Guid id);
    Task UpdateAsync(WorkspaceUserDTO workspaceUserDto);
    Task CreateAsync(WorkspaceUserDTO workspaceUserDto);
    Task DeleteAsync(Guid id);
    Task<WorkspaceUserDTO?> GetByUserLoginAsync(string userLogin);
    Task<HashSet<Permission>> GetUserPermission(Guid userID);
    Task CreateUserRoleAsync(WorkspaceUserRoleDTO workspaceUserRoleDto);
    Task DeleteUserRoleAsync(WorkspaceUserRoleDTO workspaceUserRoleDto);
    Task<IEnumerable<RoleDTO>> GetUserRolesAsync(Guid id);
}
