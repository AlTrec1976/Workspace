using Workspace.Entities.Contracts;

namespace Workspace.Entities;

public class WorkspaceTask : IWorkspaceTask
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public StatusTask Status { get; set; }
    public IEnumerable<IWorkspaceNote> Notes { get; set; }
    public IWorkspaceUser Manager { get; set; }
    public IWorkspaceUser? Employee { get; set; }

    public void ChangeStatus(StatusTask status)
    { 
        Status = status;
    }
}