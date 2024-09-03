using Workspace.Entities;
using Workspace.Entities.Contracts;

namespace Workspace.BLL.Logic
{
    public interface IWorkspaceMartService
    {
        //void ChangeStatus(IWorkspaceUser workspaceUser, IWorkspaceTask workspaceTask);
        Task<WorkspaceMartResponse> CreateWorkpaceMartAsync(WorkspaceMartRequest workspaceMartRequest);
        Task<WorkspaceTaskResponse> CreateTaskAsync(Guid id, WorkspaceTaskShortRequest workspaceTaskRequest);
    }
}