using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workspace.Entities;

namespace Workspace.BLL.Logic
{
    public interface IInviteService
    {
        Task AcceptInviteAsync(InviteDetailRequest inviteDetailRequest);
        public Task<InviteResponse> CreateAsync(InviteRequest inviteRequest);
        Task<List<InviteResponse>> GetAcceptedInvitesAsync(Guid martId);
        IAsyncEnumerable<InviteResponse> GetAllInvitesAsync();
    }
}
