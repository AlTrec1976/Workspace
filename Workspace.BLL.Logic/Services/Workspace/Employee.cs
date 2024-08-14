using Workspace.Entities;
using Workspace.Entities.Contracts;

namespace Workspace.BLL.Logic;

public class Employee(IWorkspaceUser workspaceUser, IWorkspaceTask workspaceTask) : IWorkspaceRole
{
    private readonly IWorkspaceUser _workspaceUser = workspaceUser;
    private readonly IWorkspaceTask _workspaceTask = workspaceTask;
    

    public IWorkspaceUser User { get { return _workspaceUser; } }

    public IWorkspaceTask Task { get { return _workspaceTask; } }

    public void ChangeRole()
    {
        throw new NotImplementedException();
    }
    public void ChangeStatus(IWorkspaceTask workspaceTask)
    {
        if (workspaceTask.Status == StatusTask.Assigned)
        {
            workspaceTask.ChangeStatus(StatusTask.Accepted);
        }
        if (workspaceTask.Status == StatusTask.Accepted)
        {
            workspaceTask.Status = StatusTask.JobInProgress;
        }
        if (workspaceTask.Status == StatusTask.JobInProgress)
        {
            workspaceTask.Status = StatusTask.JobDown;
        }
    }
}
