using Microsoft.AspNetCore.Mvc;
using Workspace.BLL.Logic;
using Workspace.Entities;

namespace Workspace.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkspaceSendbox (ISendboxService sendboxService): ControllerBase
    {
        private readonly ISendboxService _sendboxService=sendboxService;

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
            await _sendboxService.CreateAsync(sendboxRequest);
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
