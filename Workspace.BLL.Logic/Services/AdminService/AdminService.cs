using AutoMapper;
using Microsoft.Extensions.Logging;

using Workspace.DAL;
using Workspace.Entities;

namespace Workspace.BLL.Logic
{
    public class AdminService(AppDbContext context, IMapper mapper, IAdminRepository adminRepository, ILogger<AdminService> logger) : IAdminService
    {
        private readonly AppDbContext _context = context;
        private readonly IAdminRepository _adminRepository = adminRepository;
        private readonly IMapper _mapper = mapper;
        private readonly ILogger _logger = logger;

        #region Roles
        public async Task<RoleResponse> CreateRoleAsync(RoleRequest roleRequest)
        {
            var _role = _mapper.Map<Role>(roleRequest);
            _role.RoleId = default;

            var _roleDTO = _mapper.Map<RoleDTO>(_role);
            await _adminRepository.CreateRoleAsync(_roleDTO);
            var _roleResponse = _mapper.Map<RoleResponse>(_roleDTO);
            return _roleResponse;
        }
        public async Task UpdateRoleAsync(RoleResponse roleResponse)
        {
            var _role = _mapper.Map<Role>(roleResponse);
            var _roleDTO = _mapper.Map<RoleDTO>(_role);
            await _adminRepository.UpdateRoleAsync(_roleDTO);
        }
        public async Task DeleteRoleAsync(RoleResponse roleResponse)
        {
            var _role = _mapper.Map<Role>(roleResponse);
            var _roleDTO = _mapper.Map<RoleDTO>(_role);
            await _adminRepository.DeleteRoleAsync(_roleDTO);
        }

        public async Task<List<RoleResponse>> GetAllRolesAsync()
        { 
            var _rolesResponse = new List<RoleResponse>();
            var _rolesDTO = await _adminRepository.GetAllRolesAsync();
            _rolesResponse = _mapper.Map<List<RoleResponse>>(_rolesDTO);
            return _rolesResponse;
        }
        #endregion

        #region Permissions
        public async Task<WorkspacePermissionResponse> CreatePermissionAsync(WorkspacePermissionRequest permissionRequest)
        {
            var _permission = _mapper.Map<WorkspacePermission>(permissionRequest);
            _permission.PermissionId = default;

            var _permissionDTO = _mapper.Map<WorkspacePermissionDTO>(_permission);
            await _adminRepository.CreatePermissionAsync(_permissionDTO);
            var _permisionResponse = _mapper.Map<WorkspacePermissionResponse>(_permissionDTO);
            return _permisionResponse;
        }
        public async Task UpdatePermissionAsync(WorkspacePermissionResponse permissionResponse)
        {
            var _permission = _mapper.Map<WorkspacePermission>(permissionResponse);
            var _permissionDTO = _mapper.Map<WorkspacePermissionDTO>(_permission);
            await _adminRepository.UpdatePermissionAsync(_permissionDTO);
        }
        public async Task DeletePermissionAsync(WorkspacePermissionResponse permissionResponse)
        {
            var _permission = _mapper.Map<WorkspacePermission>(permissionResponse);
            var _permissionDTO = _mapper.Map<WorkspacePermissionDTO>(_permission);
            await _adminRepository.DeletePermisisonAsync(_permissionDTO);
        }

        public async Task<List<WorkspacePermissionResponse>> GetAllPermissionsAsync()
        {
            var _permissionsResponse = new List<WorkspacePermissionResponse>();
            var _permissionsDTO = await _adminRepository.GetAllPermissionsAsync();
            _permissionsResponse = _mapper.Map<List<WorkspacePermissionResponse>>(_permissionsDTO);
            return _permissionsResponse;
        }
        #endregion

        public async Task CreateRolePermissionAsync(RolePermissionRequest rolePermissionRequest)
        {
            try
            {
                var _rolePermissionDTO = _mapper.Map<RolePermissionDTO>(rolePermissionRequest);
                await _adminRepository.CreateRolePermissionAsync(_rolePermissionDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка в CreateRolePermissionAsync");
                throw;
            }
        }
        public async Task DeleteRolePermissionAsync(RolePermissionRequest rolePermissionRequest)
        {
            try
            {
                var _rolePermissionDTO = _mapper.Map<RolePermissionDTO>(rolePermissionRequest);
                await _adminRepository.DeleteRolePermissionAsync(_rolePermissionDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка в DeleteRolePermissionAsync");
                throw;
            }
        }
        public async Task<List<WorkspacePermissionResponse>> GetRolePermissionsAsync(int id)
        {
            try
            {
                var _permissionsResponse = new List<WorkspacePermissionResponse>();
                var _permissionsDTO = await _adminRepository.GetRolePermissionsAsync(id);
                _permissionsResponse = _mapper.Map<List<WorkspacePermissionResponse>>(_permissionsDTO);
                return _permissionsResponse;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка в GetRolePermissionsAsync");
                throw;
            }
        }
    }
}
