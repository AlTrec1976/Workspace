using Workspace.DAL;
using Workspace.BLL.Logic.Contracts;
using Workspace.Entities;
using AutoMapper;
using Microsoft.Extensions.Logging;

namespace Workspace.BLL.Logic;

public class InviteService(IMapper mapper, IInviteRepository inviteRepository, ILogger<InviteService> logger) : IInviteService
{
    private readonly IInviteRepository _inviteRepository = inviteRepository;
    private readonly IMapper _mapper = mapper;
    private readonly ILogger _logger = logger;
    
    public async Task<InviteResponse> CreateAsync(InviteRequest inviteRequest)
    {
        try
        {
            var _invite = _mapper.Map<Invite>(inviteRequest);
            _invite.Id = Guid.Empty;

            var _inviteDTO = _mapper.Map<InviteDTO>(_invite);

            var checkInvite = await _inviteRepository.CheckInviteAsync(inviteRequest.MartId);

            if (checkInvite?.Id != Guid.Empty)
            {
                throw new Exception("Приглашение уже отправлено");
            }

            _inviteDTO = await _inviteRepository.CreateInviteAsync(_inviteDTO);
            var _inviteResponse = _mapper.Map<InviteResponse>(_inviteDTO);

            return _inviteResponse;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка в CreateAsync");
            throw;
        }
    }

    public async Task AcceptInviteAsync(InviteDetailRequest inviteDetailRequest)
    {
        try
        {
            var _inviteDetailDTO = _mapper.Map<InviteDetailDTO>(inviteDetailRequest);

            await _inviteRepository.AcceptInviteAsync(_inviteDetailDTO);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка в AcceptInviteAsync");
            throw;
        }
    }
    public async IAsyncEnumerable<InviteResponse> GetAllInvitesAsync()
    {
        await foreach (var item in _inviteRepository.GetAllInvitesAsync())
        {
            yield return _mapper.Map<InviteResponse>(item);
        }
    }

    public async Task<List<InviteResponse>> GetAcceptedInvitesAsync(Guid martId)
    {
        try
        {
            var _invites = new List<InviteResponse>();
            var _invitekResponsesDTO = await _inviteRepository.GetAcceptedInvitesAsync(martId);
            _invites = _mapper.Map<List<InviteResponse>>(_invitekResponsesDTO);

            return _invites;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка в GetAcceptedInvitesAsync");
            throw;
        }
    }
}