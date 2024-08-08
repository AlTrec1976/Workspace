using Workspace.Entities.Contracts;

namespace Workspace.Entities;

public class WorkspaceTask : IWorkspaceTask
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public StatusTask Status { get; set; }
    public IEnumerable<IWorkspaceNote> Notes { get; set; }
    public Guid ManagerId { get; set; }
    public Guid? EmployeeId { get; set; }
}