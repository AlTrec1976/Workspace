using Workspace.Entities.Contracts;

namespace Workspace.Entities;

public class WorkspaceTask : IWorkspaceTask
{
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