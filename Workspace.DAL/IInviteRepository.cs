using Workspace.Entities;

namespace Workspace.DAL
{
    public interface IInviteRepository
    {
        Task AcceptInviteAsync(InviteDetailDTO inviteDetailDTO);
        Task<InviteDTO?> CheckInviteAsync(Guid id);
        Task<InviteDTO> CreateInviteAsync(InviteDTO inviteDTO);
        Task<IEnumerable<InviteRollDTO>> GetAcceptedInvitesAsync(Guid martid);
        IAsyncEnumerable<InviteRollDTO> GetAllInvitesAsync();
    }
}