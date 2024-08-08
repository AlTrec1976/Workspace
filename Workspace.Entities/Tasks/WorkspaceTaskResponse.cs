namespace Workspace.Entities;

public class WorkspaceTaskResponse : WorkspaceTaskRequest
{
    public Guid Id { get; set; }
    public string StatusName { get; set; }
}