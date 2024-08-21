using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Workspace.BLL.Logic;
using Workspace.Entities;

namespace Workspace.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkspaceSendbox (ISendboxService sendboxService, ILogger<WorkspaceSendbox> logger) : ControllerBase
    {
        private readonly ISendboxService _sendboxService=sendboxService;
        private readonly ILogger _logger = logger;

        /// <summary>
        /// Добавляем пользователя в песочницу
        /// </summary>
        /// <remarks>
        /// Добавляем пользователя в песочницу по конкретный март,
        /// чтобы из песочницы уже добавить к таску
        /// </remarks>
        /// <param name="sendboxRequest"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task CreateAsync([FromBody] SendboxRequest sendboxRequest)
        {
            try
            {
                var validator = new SendboxRequestValidator();

                var validationResult = validator.Validate(sendboxRequest);

                if (!validationResult.IsValid)
                {
                    var error = string.Empty;

                    foreach (var item in validationResult.Errors)
                    {
                        error += $"{item} \n";
                    }

                    throw new Exception(error);
                }

                await _sendboxService.CreateAsync(sendboxRequest);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка в ");
                throw;
            }
        }

        /// <summary>
        /// Возвращает пользователей отбранных из песочницы
        /// </summary>
        /// <remarks>
        /// Данные метод возвращает пользоваетелей из песочницы, которые могут
        /// быть закреплены за таском
        /// </remarks>
        /// <param name="martId"></param>
        /// <returns></returns>
        [HttpGet("{martId}")]
        public async Task<List<SendboxFullRequest>> GetUsersAsync(Guid martId)
        { 
            return await _sendboxService.GetUsersAsync(martId);
        }
    }
}
