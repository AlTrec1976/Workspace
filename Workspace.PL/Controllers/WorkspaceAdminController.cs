﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Workspace.BLL.Logic;
using Workspace.Entities;

namespace Workspace.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkspaceAdminController(IAdminService adminService, ILogger<WorkspaceAdminController> logger) : ControllerBase
    {
        private readonly IAdminService _adminService = adminService;
        private readonly ILogger _logger = logger;

        #region Roles
        /// <summary>
        /// Создание роли в системе
        /// </summary>
        /// <param name="roleRequest"></param>
        /// <returns></returns>
        [Authorize]
        [HasPermission([Permission.sysadmin])]
        [HttpPost("/roles")]
        public async Task<RoleResponse> CreateRoleAsync([FromBody] RoleRequest roleRequest)
        {
            try
            {
                var validator = new RoleRequestValidator();

                var validationResult = validator.Validate(roleRequest);

                if (!validationResult.IsValid)
                {
                    var error = string.Empty;

                    foreach (var item in validationResult.Errors)
                    {
                        error += $"{item} \n";
                    }

                    throw new Exception(error);
                }

                return await _adminService.CreateRoleAsync(roleRequest);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка в CreateRoleAsync");
                throw;
            }
        }
        /// <summary>
        /// Изменение названия роли в системе
        /// </summary>
        /// <param name="roleResponse"></param>
        /// <returns></returns>
        [Authorize]
        [HasPermission([Permission.sysadmin])]
        [HttpPut("/roles")]
        public async Task UpdateRoleAsync([FromBody] RoleResponse roleResponse)
        {
            try
            {
                var validator = new RoleResponseValidator();

                var validationResult = validator.Validate(roleResponse);

                if (!validationResult.IsValid)
                {
                    var error = string.Empty;

                    foreach (var item in validationResult.Errors)
                    {
                        error += $"{item} \n";
                    }

                    throw new Exception(error);
                }

                await _adminService.UpdateRoleAsync(roleResponse);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка в UpdateRoleAsync");
                throw;
            }
        }

        /// <summary>
        /// Удаление роли из системы
        /// </summary>
        /// <remarks>
        ///  Каскадное удаление роли, также удалит данную роль у всех пользователей системы и
        ///  сопоставление "роль - разрешения"
        /// </remarks>
        /// <param name="id">Идентификатор роли</param>
        /// <returns></returns>
        [Authorize]
        [HasPermission([Permission.sysadmin])]
        [HttpDelete("/roles/{id}")]
        public async Task DeleteRoleAsync(int id)
        {
            try
            {
                var _response = new RoleResponse() { RoleId=id};
                await _adminService.DeleteRoleAsync(_response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка в DeleteRoleAsync");
                throw;
            }
        }

        /// <summary>
        /// Все роли существующие в системе
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HasPermission([Permission.admin, Permission.read])]
        [HttpGet("/roles")]
        public async Task<List<RoleResponse>> GetAllRolesAsync()
        {
            try
            {
                return await _adminService.GetAllRolesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка в GetAllRolesAsync");
                throw;
            }
        }
        #endregion

        #region Permissions

        /// <summary>
        /// Создание разрешения в системе 
        /// </summary>
        /// <remarks>
        /// Идентификатор разрешения должен совпадать с перечислением в enum Permission 
        /// </remarks>
        /// <param name="permissionRequest"></param>
        /// <returns></returns>
        [Authorize]
        [HasPermission([Permission.sysadmin])]
        [HttpPost("/permissions")]
        public async Task<WorkspacePermissionResponse> CreatePermissionAsync([FromBody] WorkspacePermissionRequest permissionRequest) 
        {
            try
            {
                var validator = new PermissionRequestValidator();

                var validationResult = validator.Validate(permissionRequest);

                if (!validationResult.IsValid)
                {
                    var error = string.Empty;

                    foreach (var item in validationResult.Errors)
                    {
                        error += $"{item} \n";
                    }

                    throw new Exception(error);
                }

                return await _adminService.CreatePermissionAsync(permissionRequest);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка в CreatePermissionAsync");
                throw;
            }
        }

        /// <summary>
        /// Изменение названия разрешения
        /// </summary>
        /// <param name="permissionResponse"></param>
        /// <returns></returns>
        [Authorize]
        [HasPermission([Permission.sysadmin])]
        [HttpPut("/permissions")]
        public async Task UpdatePermissionAsync([FromBody] WorkspacePermissionResponse permissionResponse)
        {
            try
            {
                var validator = new PermissionResponseValidator();

                var validationResult = validator.Validate(permissionResponse);

                if (!validationResult.IsValid)
                {
                    var error = string.Empty;

                    foreach (var item in validationResult.Errors)
                    {
                        error += $"{item} \n";
                    }

                    throw new Exception(error);
                }

                await _adminService.UpdatePermissionAsync(permissionResponse);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка в UpdatePermissionAsync");
                throw;
            }
        }
        /// <summary>
        /// Удаление разрешения 
        /// </summary>
        /// <remarks>
        /// Каскадное удаление разрешения. Также удалиться сопоставление "роли-разрешение"
        /// </remarks>
        /// <param name="id">Идентификатор разрешения</param>
        /// <returns></returns>
        [Authorize]
        [HasPermission([Permission.sysadmin])]
        [HttpDelete("/permissions/{id}")]
        public async Task DeletePermission(int id)
        {
            try
            {
                var _response = new WorkspacePermissionResponse() { PermissionId=id};
                await _adminService.DeletePermissionAsync(_response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка в DeletePermission");
                throw;
            }
        }

        /// <summary>
        /// Возвращает все разрешения в системе
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HasPermission([Permission.sysadmin])]
        [HttpGet("/permissions")]
        public async Task<List<WorkspacePermissionResponse>> GetAllPermissionsAsync()
        {
            try
            {
                return await _adminService.GetAllPermissionsAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка в GetAllPermissionsAsync");
                throw;
            }
        }
        #endregion

        #region Setting access rights

        /// <summary>
        /// Создание связи "роль - разрешение"
        /// </summary>
        /// <param name="rolePermissionRequest"></param>
        /// <returns></returns>
        [Authorize]
        [HasPermission([Permission.sysadmin])]
        [HttpPost]
        public async Task CreateRolePermissionAsync([FromBody] RolePermissionRequest rolePermissionRequest)
        {
            try
            {
                await _adminService.CreateRolePermissionAsync(rolePermissionRequest);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка в CreateRolePermissionAsync");
                throw;
            }
        }
        /// <summary>
        /// Удаление связи "роль - разрешение"
        /// </summary>
        /// <param name="rolePermissionRequest"></param>
        /// <returns></returns>
        [Authorize]
        [HasPermission([Permission.sysadmin])]
        [HttpDelete]
        public async Task DeleteRolePermissionAsync([FromBody] RolePermissionRequest rolePermissionRequest)
        {
            try
            {
                await _adminService.DeleteRolePermissionAsync(rolePermissionRequest);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка в DeleteRolePermissionAsync");
                throw;
            }
        }

        /// <summary>
        /// Отображаются все разрешения для роли
        /// </summary>
        /// <param name="id">Идентификатор роли</param>
        /// <returns></returns>
        [Authorize]
        [HasPermission([Permission.sysadmin])]
        [HttpGet("{id}")]
        public async Task<List<WorkspacePermissionResponse>> GetRolePermissionsAsync(int id)
        {
            try
            {
                return await _adminService.GetRolePermissionsAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка в GetRolePermissionsAsync");
                throw;
            }
        }

        #endregion
    }
}