using Workspace.Entities;

namespace Workspace.DAL
{
    public interface IAdminRepository
    {
        Task<WorkspacePermissionDTO> CreatePermissionAsync(WorkspacePermissionDTO permissionDto);
        Task<RoleDTO> CreateRoleAsync(RoleDTO roleDto);
        Task CreateRolePermissionAsync(RolePermissionDTO rolePermissionDto);
        Task DeletePermisisonAsync(WorkspacePermissionDTO permissionDto);
        Task DeleteRoleAsync(RoleDTO roleDto);
        Task DeleteRolePermissionAsync(RolePermissionDTO rolePermissionDto);
        Task<IEnumerable<WorkspacePermissionDTO>> GetAllPermissionsAsync();
        Task<IEnumerable<RoleDTO>> GetAllRolesAsync();
        Task<IEnumerable<WorkspacePermissionDTO>> GetRolePermissionsAsync(int id);
        Task UpdatePermissionAsync(WorkspacePermissionDTO permissionDto);
        Task UpdateRoleAsync(RoleDTO roleDto);
    }
}