using Workspace.Entities;

namespace Workspace.BLL.Logic
{
    public interface IAdminService
    {
        Task<WorkspacePermissionResponse> CreatePermissionAsync(WorkspacePermissionRequest permissionRequest);
        Task<RoleResponse> CreateRoleAsync(RoleRequest roleRequest);
        Task CreateRolePermissionAsync(RolePermissionRequest rolePermissionRequest);
        Task DeletePermissionAsync(WorkspacePermissionResponse permissionResponse);
        Task DeleteRoleAsync(RoleResponse roleResponse);
        Task DeleteRolePermissionAsync(RolePermissionRequest rolePermissionRequest);
        Task<List<WorkspacePermissionResponse>> GetAllPermissionsAsync();
        Task<List<RoleResponse>> GetAllRolesAsync();
        Task<List<WorkspacePermissionResponse>> GetRolePermissionsAsync(int id);
        Task UpdatePermissionAsync(WorkspacePermissionResponse permissionResponse);
        Task UpdateRoleAsync(RoleResponse roleResponse);
    }
}