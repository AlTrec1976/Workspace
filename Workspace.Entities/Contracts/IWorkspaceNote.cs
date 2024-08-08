namespace Workspace.Entities.Contracts;

public interface IWorkspaceNote
{
    public Guid Id { get; set; }

    public string Note { get; set; }
    public Guid UserId { get; set; }
    public Guid TaskId { get; set; }
}
