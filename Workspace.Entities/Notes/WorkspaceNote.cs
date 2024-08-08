using Workspace.Entities.Contracts;

namespace Workspace.Entities;

public class WorkspaceNote : IWorkspaceNote
{
    public Guid Id { get; set; }
    public string Note { get; set; }
    public Guid UserId { get; set; }
    public Guid TaskId { get; set; }
}
