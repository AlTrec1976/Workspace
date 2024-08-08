using Workspace.Entities;

namespace Workspace.DAL;

public interface IUserRepository
{
    Task<IEnumerable<WorkspaceUserDTO>> GetAllUsersAsync();
    Task<WorkspaceUserDTO> GetByIDAsync(Guid id);
    Task UpdateAsync(WorkspaceUserDTO workspaceUserDto);
    Task CreateAsync(WorkspaceUserDTO workspaceUserDto);
    Task DeleteAsync(Guid id);
    Task<WorkspaceUserDTO?> GetByUserLoginAsync(string userLogin);
    Task<HashSet<Permission>> GetUserPermission(Guid userID);

}
