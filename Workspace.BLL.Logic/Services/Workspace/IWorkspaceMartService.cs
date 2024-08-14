using Workspace.Entities;
using Workspace.Entities.Contracts;

namespace Workspace.BLL.Logic
{
    public interface IWorkspaceMartService
    {
        void AddUserToWorkspace();
        void ChangeStatus(IWorkspaceUser workspaceUser, IWorkspaceTask workspaceTask);
        void CreateInvite();
        Task<WorkspaceMartResponse> CreateWorkpaceMartAsync(WorkspaceMartRequest workspaceMartRequest);
        Task<WorkspaceTaskResponse> CreateTaskAsync(Guid id, WorkspaceTaskShortRequest workspaceTaskRequest);
        void GetWorkspaceForOwners();
        void GetWorkspaceForUsers();
    }
}