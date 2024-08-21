using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;
using Workspace.BLL.Logic;
using Workspace.Entities;

namespace Workspace.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkspaceInviteController(IInviteService inviteService, ILogger<WorkspaceInviteController> logger) : ControllerBase
    {
        
        private readonly IInviteService _inviteService = inviteService;
        private readonly ILogger _logger = logger;

        /// <summary>
        /// Создание приглашения
        /// </summary>
        /// <remarks>
        /// Пользователь, он же менеджер, который создал WorkspaceMart, выкладывает приглашение,
        /// чтобы другие пользователи системы могли поучаствовать в тасках по этому марту
        /// </remarks>
        /// <param name="inviteRequest"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<InviteResponse> CreateAsync([FromBody] InviteRequest inviteRequest)
        {
            try
            {
                var validator = new InviteRequestValidator();

                var validationResult = validator.Validate(inviteRequest);

                if (!validationResult.IsValid)
                {
                    var error = string.Empty;

                    foreach (var item in validationResult.Errors)
                    {
                        error += $"{item} \n";
                    }

                    throw new Exception(error);
                }

                return await _inviteService.CreateAsync(inviteRequest);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка в CreateAsync");
                throw;
            }
        }

        /// <summary>
        /// Список приглашений для участия в тасках
        /// </summary>
        /// <remarks>
        ///  Выводит список приглашений для пользователей, чтобы была возможность
        ///  пользователю добавить себя в песочницу из который менеджер будет
        ///  выбирать конкретных пользователей для добавления в таск
        /// </remarks>
        /// <returns></returns>
        [HttpGet("/GetAllInvites")]
        public async Task<List<InviteResponse>> GetAllInvitesAsync()
        {
            try
            {
                return await _inviteService.GetAllInvitesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка в GetAllInvitesAsync");
                throw;
            }
        }

        /// <summary>
        /// Принятие приглашения одним из пользователей
        /// </summary>
        /// <remarks>
        /// Если пользователь принимает приглашение, после этого менеджер может 
        /// его добавить в песочницу. Так как пользователь закрепляется в таске
        /// только из песочницы
        /// </remarks>
        /// <param name="inviteDetailRequest">
        ///     "inviteID": приглашение на которое согласен пользователь,
        ///     "userID": идентификатор пользователя ,
        ///     "comments": комментарий, который может оставить пользователь
        /// </param>
        /// <returns></returns>
        /// <response code="200">Ответ на приглашение сохранено, менеджер 
        /// сможет добавить в песочницу данного пользователя</response>
        [HttpPost("/AcceptInvite")]
        public async Task AcceptInvite([FromBody] InviteDetailRequest? inviteDetailRequest)
        {
            try
            {
                var validator = new InviteDetailRequestValidator();

                var validationResult = validator.Validate(inviteDetailRequest);

                if (!validationResult.IsValid)
                {
                    var error = string.Empty;

                    foreach (var item in validationResult.Errors)
                    {
                        error += $"{item} \n";
                    }

                    throw new Exception(error);
                }

                await _inviteService.AcceptInviteAsync(inviteDetailRequest);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка в AcceptInvite");
                throw;
            }
        }

        /// <summary>
        /// Список пользователей которые приняли приглашение. 
        /// </summary>
        /// <remarks>
        /// Из данного списка менеджер будет добавлять пользователей в песочницу,
        /// из которой можно уже выбрать пользователей и закрепить за конкретным
        /// такско
        /// </remarks>
        /// <param name="martId">ИД марта, для которых хотим увидеть согласившихся юзеров</param>
        /// <returns> </returns>
        [HttpGet("/AcceptInvite/{martId}")]
        public async Task<List<InviteResponse>> GetAcceptedInvitesAsync(Guid martId)
        {
            try
            {
                return await _inviteService.GetAcceptedInvitesAsync(martId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка в GetAcceptedInvitesAsync");
                throw;
            }
        }
    }
}
