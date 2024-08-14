using Workspace.Entities;

namespace Workspace.DAL
{
    public interface IInviteRepository
    {
        Task AcceptInviteAsync(InviteDetailDTO inviteDetailDTO);
        Task<InviteDTO> CreateInviteAsync(InviteDTO inviteDTO);
        Task<IEnumerable<InviteRollDTO>> GetAcceptedInvitesAsync(Guid martid);
        Task<IEnumerable<InviteRollDTO>> GetAllInvitesAsync();
    }
}