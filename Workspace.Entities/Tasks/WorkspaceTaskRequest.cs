namespace Workspace.Entities;

public class WorkspaceTaskRequest
{
    public string Name { get; set; } = string.Empty;
    public int Status { get; set; } = 1;
    public Guid ManagerId { get; set; } = Guid.Empty;
    public Guid? EmployeeId { get; set; } = Guid.Empty;
}