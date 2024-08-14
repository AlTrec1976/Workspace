using Workspace.Entities;

namespace Workspace.DAL
{
    public interface ISendboxRepository
    {
        Task CreateSendboxAsync(SendboxDTO sendboxDTO);
    }
}