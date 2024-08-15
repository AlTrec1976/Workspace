using Workspace.DAL;
using Workspace.BLL.Logic.Contracts;
using Workspace.Entities;
using AutoMapper;
using Microsoft.Extensions.Logging;

namespace Workspace.BLL.Logic;

public class InviteService(AppDbContext context, IMapper mapper, IInviteRepository inviteRepository, ILogger<TaskService> logger) : IInviteService
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

            //var _invite = new Invite()
            //{ 
            //    Id = Guid.Empty,
            //    InviteText = inviteRequest.InviteText,
            //    WorkspaceMartObj = new WorkspaceMart() { Id = inviteRequest.MartId},
            //    IsOppened = true
            //};

            var _inviteDTO = _mapper.Map<InviteDTO>(_invite);

            //var _inviteDTO = new InviteDTO() 
            //{
            //    InviteText = _invite.InviteText,
            //    IsOppened = _invite.IsOppened,
            //    MartId = _invite.WorkspaceMartObj.Id
            //};


            ///TODO: Перед добавлением, нужно убедиться, что для марта уже не создано приглашение.
            /// Так как на 1 март, должно быть 1 приглашение.
            /// НАПИСАЛ, но нужно убедиться, правильно ли понял откуда брать
            
            var checkInvite = _inviteRepository.CheckInviteAsync(_inviteDTO);

            if (checkInvite is not null)
            {
                throw new Exception("Приглашение уже отправлено");
            }
            
            await _inviteRepository.CreateInviteAsync(_inviteDTO);
            var _inviteResponse = _mapper.Map<InviteResponse>(_inviteDTO);

            return _inviteResponse;

            //return new InviteResponse()
            //{
            //    InviteText = _inviteDTO.InviteText,
            //    IsOppened = _inviteDTO.IsOppened,
            //    MartId = _inviteDTO.MartId,
            //    UserId = _inviteDTO.UserId,
            //    Id = _inviteDTO.Id
            //};
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка в CreateAsync");
            throw;
        }
    }

    public async Task AcceptInviteAsync(InviteDetailRequest inviteDetailRequest)
    {
        var _inviteDetailDTO = _mapper.Map<InviteDetailDTO>(inviteDetailRequest);

        //var _inviteDetailDTO = new InviteDetailDTO() 
        //{
        //    Comments = inviteDetailRequest.Comments,
        //    InviteID = inviteDetailRequest.InviteID,
        //    UserID = inviteDetailRequest.UserID
        //};

        await _inviteRepository.AcceptInviteAsync(_inviteDetailDTO);
    }
    public async Task<List<InviteResponse>> GetAllInvitesAsync()
    {
        var _invites = new List<InviteResponse>();
        var _invitekResponsesDTO = await _inviteRepository.GetAllInvitesAsync();
         _invites = _mapper.Map<List<InviteResponse>> (_invitekResponsesDTO);
        return _invites;
    }

    public async Task<List<InviteResponse>> GetAcceptedInvitesAsync(Guid martId)
    {
        var _invites = new List<InviteResponse>();
        var _invitekResponsesDTO = await _inviteRepository.GetAcceptedInvitesAsync(martId);
        _invites = _mapper.Map<List<InviteResponse>>(_invitekResponsesDTO);
        return _invites;
    }
}