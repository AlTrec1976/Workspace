using Workspace.Entities.Contracts;


namespace Workspace.Entities;

public class WorkspaceTask : IWorkspaceTask
{
    private WorkspaceUser _user;
    private WorkspaceTask _task;
    private IOrgRole _role;
    public WorkspaceTask() {}
    public WorkspaceTask(WorkspaceUser workspace, WorkspaceTask workspaceTask)
    {

    }
    public Guid Id { get; set; }
    public string Name { get; set; }
    public StatusTask Status { get; set; }
    public IEnumerable<WorkspaceNote> Notes { get; set; }
    public WorkspaceUser Manager { get; set; }
    public WorkspaceUser? Employee { get; set; }

    public void ChangeStatus(StatusTask status)
    { 
        Status = status;
    }
}