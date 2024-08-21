using Workspace.DAL;
using Workspace.BLL.Logic.Contracts;
using Workspace.Entities;
using AutoMapper;
using Microsoft.Extensions.Logging;

namespace Workspace.BLL.Logic;

public class InviteService(AppDbContext context, IMapper mapper, IInviteRepository inviteRepository, ILogger<InviteService> logger) : IInviteService
{
    private readonly AppDbContext _context = context;
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

            var checkInvite = _inviteRepository.CheckInviteAsync(_inviteDTO.MartId);

            if (checkInvite is not null)
            {
                throw new Exception("Приглашение уже отправлено");
            }
            
            await _inviteRepository.CreateInviteAsync(_inviteDTO);
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
    public async Task<List<InviteResponse>> GetAllInvitesAsync()
    {
        try
        {
            var _invites = new List<InviteResponse>();
            var _invitekResponsesDTO = await _inviteRepository.GetAllInvitesAsync();
             _invites = _mapper.Map<List<InviteResponse>> (_invitekResponsesDTO);

            return _invites;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка в GetAllInvitesAsync");
            throw;
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