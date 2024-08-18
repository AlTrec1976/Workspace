using AutoMapper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workspace.BLL.Logic.Contracts;
using Workspace.DAL;
using Workspace.Entities;
using Workspace.Entities.Contracts;

namespace Workspace.BLL.Logic
{
    public class SendboxService(AppDbContext context, IMapper mapper, ISendboxRepository sendboxRepository, ILogger<TaskService> logger)
                                : ISendboxService
    {
        private readonly AppDbContext _context = context;
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
            var senboxResponse = new List<SendboxFullRequest>();
            var _usersDTO = await _sendboxRepository.GetUsersAsync(martId);
            senboxResponse = _mapper.Map<List<SendboxFullRequest>>(_usersDTO);
            return senboxResponse;
        }
    }
}
