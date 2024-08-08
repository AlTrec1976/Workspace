using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Workspace.BLL.Logic;
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
    [Authorize]
    [AllowAnonymous] // ,ЗАЧЕМ????
    [HttpPost("api/login")]
    //[HttpGet("{login}/{password}")]
    public async Task<ActionResult> LoginAsync([FromBody] WorkspaceUserLogin userLogin)
    {
        try
        {
            var _workspaceUserRequest = new WorkspaceUserRequest();
            _workspaceUserRequest.Login = userLogin.Login;
            _workspaceUserRequest.Password = userLogin.Password;
            //создать токен
            var _token = await _userService.LoginAsync(_workspaceUserRequest);

            Response.Cookies.Append("maxima-sec-cookies", _token, new CookieOptions
            {
                Expires = DateTimeOffset.UtcNow.AddDays(5),
                HttpOnly = true,
                IsEssential = true,
                Secure = true,
                SameSite = SameSiteMode.None
            });

            //return Ok(_token);
            return Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка в LoginAsync");
            throw;
        }
    }

    // GET: api/<WorkspaceUserController>
    [Authorize]
    [HasPermission([Permission.create, Permission.update])]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<WorkspaceUserResponse>>> GetAsync()
    {
        try
        {
            var _workspaceUserResponse = await _userService.GetAllAsync();
            return Ok(_workspaceUserResponse);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка в GetAsync");
            throw;
        }
    }

    // GET api/<WorkspaceUserController>/5
    [Authorize]
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
    [Authorize]
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
    [Authorize]
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
    [Authorize]
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
}
