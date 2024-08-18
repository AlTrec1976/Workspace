namespace Workspace.Entities.Contracts;

public interface IWorkspaceTask
{
    public Guid Id { get; set; }

    public string Name { get; set; }
    public StatusTask Status { get; set; }
    public IEnumerable<WorkspaceNote> Notes { get; set; }
    public WorkspaceUser Manager { get; set; }
    public WorkspaceUser? Employee { get; set; }

    void ChangeStatus(StatusTask status);
    
}