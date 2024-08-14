using Workspace.Entities;

namespace Workspace.DAL
{
    public interface IMartRepository
    {
        Task<WorkspaceMartDTO> CreateMartAsync(WorkspaceMartDTO workspaceMartDTO);
        Task<WorkspaceTaskDTO> CreateTaskAsync(WorkspaceMartDTO workspaceMartDTO, WorkspaceTaskDTO workspaceTaskDTO);
    }
}