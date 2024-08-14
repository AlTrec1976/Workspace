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
        public async Task CreateAsync([FromBody] SendboxRequest sendboxRequest)
        {
            await _sendboxService.CreateAsync(sendboxRequest);
        }
    }
}
