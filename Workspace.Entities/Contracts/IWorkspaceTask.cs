namespace Workspace.Entities.Contracts;

public interface IWorkspaceTask
{
    public Guid Id { get; set; }

    public string Name { get; set; }
    public StatusTask Status { get; set; }
    public IEnumerable<IWorkspaceNote> Notes { get; set; }
    public IWorkspaceUser Manager { get; set; }
    public IWorkspaceUser? Employee { get; set; }

    void ChangeStatus(StatusTask status);
    
}