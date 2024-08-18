using Workspace.Entities;

namespace Workspace.BLL.Logic
{
    public interface ISendboxService
    {
        Task CreateAsync(SendboxRequest sendboxRequest);
        Task<List<SendboxFullRequest>> GetUsersAsync(Guid martId);
    }
}