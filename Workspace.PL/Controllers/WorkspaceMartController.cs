using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Workspace.BLL.Logic;
using Workspace.Entities;


namespace Workspace.PL.Controllers
{
    /// <summary>
    /// Контроллер работы с WorkspaceMart
    /// </summary>
    /// <param name="workspaceMartService"></param>
    [Route("api/[controller]")]
    [ApiController]
    public class WorkspaceMartController(IWorkspaceMartService workspaceMartService, ILogger<WorkspaceMartController> logger) : ControllerBase
    {
        private readonly IWorkspaceMartService _workspaceMartService = workspaceMartService;
        private readonly ILogger _logger = logger;

        /// <summary>
        /// Создание WorkspaceMart
        /// </summary>
        /// <remarks>
        /// Пользователь создает WorkspaceMart в котором будут Таски и пользователи 
        /// закрепленные за тасками
        /// </remarks>
        /// <param name="workspaceMartRequest">
        /// "name": название WorkspaceMart,
        ///"ownerId": ИД пользователя, который создает 
        /// </param>
        /// <response code="200">Вернет JSON workspaceMartRequestе который содержит ИД созданного  WorkspaceMart</response>
        [Authorize]
        [HasPermission([Permission.manager, Permission.create])]
        [HttpPost]
        public async Task <WorkspaceMartResponse> CreateAsyncAsync([FromBody] WorkspaceMartRequest workspaceMartRequest)
        {
            try
            {
                var validator = new MartRequestValidator();

                var validationResult = validator.Validate(workspaceMartRequest);

                if (!validationResult.IsValid)
                {
                    var error = string.Empty;

                    foreach (var item in validationResult.Errors)
                    {
                        error += $"{item} \n";
                    }

                    throw new Exception(error);
                }

                return await _workspaceMartService.CreateWorkpaceMartAsync(workspaceMartRequest);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка в CreateAsyncAsync");
                throw;
            }
        }

        /// <summary>
        /// Добавление Таска связанного с WorkspaceMart
        /// </summary>
        /// <remarks>
        /// На данном этапе не может быть добавлен пользователь, так как пользователь может
        /// быть выбран из песочницы
        /// </remarks>
        /// <param name="id">ИД WorkspaceMart к которому привязываем задания</param>
        /// <param name="workspaceTaskRequest">
        ///   "name": название нового Таска, 
        ///   "status": согласно логике при создании Таска его статус равен Новый = 1
        /// </param>
        /// <returns></returns>
        [Authorize]
        [HasPermission([Permission.manager, Permission.create])]
        [HttpPost("{id}")]
        public async Task<WorkspaceTaskResponse> AddNewTaskAsync(Guid id, [FromBody] WorkspaceTaskShortRequest workspaceTaskRequest)
        {
            try
            {
                var validator = new TaskShortRequestValidator();

                var validationResult = validator.Validate(workspaceTaskRequest);

                if (!validationResult.IsValid)
                {
                    var error = string.Empty;

                    foreach (var item in validationResult.Errors)
                    {
                        error += $"{item} \n";
                    }

                    throw new Exception(error);
                }

                return await _workspaceMartService.CreateTaskAsync(id, workspaceTaskRequest);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка в AddNewTaskAsync");
                throw;
            }
        }
    }
}
