using Workspace.Entities.Contracts;

namespace Workspace.Entities;

public class WorkspaceTaskRequest
{
    public string Name { get; set; }
    public int Status { get; set; }
    public IEnumerable<IWorkspaceNote> Notes { get; set; }
    public Guid ManagerId { get; set; }
    public Guid? EmployeeId { get; set; } = Guid.Empty;
}