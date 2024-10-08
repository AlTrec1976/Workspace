﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

using Workspace.BLL.Logic.Contracts;
using Workspace.Entities;
using Workspace.Entities.Users;

namespace Workspace.PL;

[Route("api/[controller]")]
[ApiController]
public class WorkspaceUserController(IUserService userService, ILogger<WorkspaceUserController> logger) : ControllerBase
{
    private readonly IUserService _userService = userService;
    private readonly ILogger _logger = logger;

    /// <summary>
    /// Вход пользователя в систему
    /// </summary>
    /// <remarks>
    /// Пример по документации контроллеров
    /// </remarks>
    /// <param name="login">Логин пользователя</param>
    /// <param name="password">Пароль пользователя</param>
    /// <returns>Возваращает cookie с токеном</returns>
    /// <response code="200">Успешное выполнение</response>
    /// <response code="400">Ошибка API</response>
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(WorkspaceUserRequest), (int)HttpStatusCode.BadRequest)]
    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> LoginAsync([FromBody] WorkspaceUserLogin userLogin)
    {
        try
        {
            var _workspaceUserRequest = new WorkspaceUserRequest();
            _workspaceUserRequest.Login = userLogin.Login;
            _workspaceUserRequest.Password = userLogin.Password;
            //создать токен
            var _token = await _userService.LoginAsync(_workspaceUserRequest);
       
            return Ok(_token);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка в LoginAsync");
            throw;
        }
    }

    // GET: api/<WorkspaceUserController>
    /// <summary>
    /// Запросить всех пользователей
    /// </summary>
    [Authorize]
    [HasPermission([Permission.admin,Permission.read])]
    [HttpGet]
    public IAsyncEnumerable<WorkspaceUserResponse> GetAsync()
    {
        return _userService.GetAllAsync();
    }

    // GET api/<WorkspaceUserController>/5
    /// <summary>
    /// Запрос пользователя по ID
    /// </summary>
    [Authorize]
    [HasPermission([Permission.admin, Permission.read])]
    [HttpGet("{id}")]
    public async Task<ActionResult<WorkspaceUserResponse>> GetAsync(Guid id)
    {
        try
        {
            var _workspaceUserResponse = await _userService.GetByIdAsync(id);

            if (_workspaceUserResponse is null)
                return NotFound();

            return Ok(_workspaceUserResponse);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка в GetAsync по id");
            throw;
        }
    }

    // PUT api/<WorkspaceUserController>/5
    /// <summary>
    /// Изменение данных пользователя
    /// </summary>
    [Authorize]
    [HasPermission([Permission.admin, Permission.update])]
    [HttpPut("{id}")]
    public async Task UpdateAsync(Guid id, [FromBody] WorkspaceUserRequest workspaceUserRequest)
    {
        try
        {
            var validator = new UserValidator();

            var validationResult = validator.Validate(workspaceUserRequest);

            if (!validationResult.IsValid)
            {
                var error = string.Empty;

                foreach (var item in validationResult.Errors)
                {
                    error += $"{item} \n";
                }

                throw new Exception(error);
            }

            await _userService.UpdateAsync(id, workspaceUserRequest);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка в UpdateAsync");
            throw;
        }
    }
    // POST api/<WorkspaceUserController>
    /// <summary>
    /// Создание пользователя
    /// </summary>
    [Authorize]
    [HasPermission([Permission.admin, Permission.create])]
    [HttpPost]
    public async Task CreateAsync([FromBody] WorkspaceUserRequest workspaceUserRequest)
    {
        try
        {
            var validator = new UserValidator();
            var validationResult = validator.Validate(workspaceUserRequest);

            if (!validationResult.IsValid)
            {
                var error = string.Empty;

                foreach (var item in validationResult.Errors)
                {
                    error += $"{item} \n";
                }

                throw new Exception(error);
            }

            await _userService.CreateAsync(workspaceUserRequest);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка в CreateAsync");
            throw;
        }
    }

    // DELETE api/<WorkspaceUserController>/5
    /// <summary>
    /// Удаление пользователя
    /// </summary>
    [Authorize]
    [HasPermission([Permission.admin, Permission.delete])]
    [HttpDelete("{id}")]
    public async Task DeleteAsync(Guid id)
    {
        try
        {
            await _userService.DeleteAsync(id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка в DeleteAsync");
            throw;
        }
    }

    /// <summary>
    /// Назначение пользователю роли
    /// </summary>
    /// <param name="workspaceUserRoleRequest"></param>
    /// <returns></returns>
    [Authorize]
    [HasPermission([Permission.admin, Permission.create])]
    [HttpPost("roles")]
    public async Task CreateRolePermissionAsync([FromBody] WorkspaceUserRoleRequest workspaceUserRoleRequest)
    {
        await _userService.CreateUserRoleAsync(workspaceUserRoleRequest);
    }

    /// <summary>
    /// Удаление роли у пользователя
    /// </summary>
    /// <param name="workspaceUserRoleRequest"></param>
    /// <returns></returns>
    [Authorize]
    [HasPermission([Permission.admin, Permission.delete])]
    [HttpDelete("roles")]
    public async Task DeleteRolePermissionAsync([FromBody] WorkspaceUserRoleRequest workspaceUserRoleRequest)
    {
        await _userService.DeleteUserRoleAsync(workspaceUserRoleRequest);
    }

    /// <summary>
    /// Возвращает все роли назначенные пользователю
    /// </summary>
    /// <param name="id">Ид пользователя</param>
    /// <returns></returns>
    [Authorize]
    [HasPermission([Permission.admin, Permission.read])]
    [HttpGet("roles")]
    public async Task<List<RoleResponse>> GetUserRolesAsync(Guid id)
    {
        return await _userService.GetAllRolesAsync(id);
    }
}
