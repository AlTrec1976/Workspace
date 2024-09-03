using AutoMapper;
using Microsoft.Extensions.Logging;
using Workspace.DAL;
using Workspace.Entities;

namespace Workspace.BLL.Logic
{
    public class SendboxService(IMapper mapper, ISendboxRepository sendboxRepository, ILogger<SendboxService> logger)
                                : ISendboxService
    {
        private readonly ISendboxRepository _sendboxRepository = sendboxRepository;
        private readonly IMapper _mapper = mapper;
        private readonly ILogger _logger = logger;

        public async Task CreateAsync(SendboxRequest sendboxRequest)
        {
            try
            {
                var _sendboxDTO = new SendboxDTO()
                { 
                    MartId = sendboxRequest.MartId,
                    UserId = sendboxRequest.UserId,
                    InviteId = sendboxRequest.InviteId
                };


                await _sendboxRepository.CreateSendboxAsync(_sendboxDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка в CreateAsync");
                throw;
            }
        }

        public async Task<List<SendboxFullRequest>> GetUsersAsync(Guid martId)
        {
            try
            {
                var senboxResponse = new List<SendboxFullRequest>();
                var _usersDTO = await _sendboxRepository.GetUsersAsync(martId);
                senboxResponse = _mapper.Map<List<SendboxFullRequest>>(_usersDTO);

                return senboxResponse;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка в GetUsersAsync");
                throw;
            }
        }
    }
}
