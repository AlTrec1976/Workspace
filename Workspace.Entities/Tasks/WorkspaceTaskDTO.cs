namespace Workspace.Entities;

public class WorkspaceTaskDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int Status { get; set; }
    public Guid ManagerId { get; set; }
    public Guid? EmployeeId { get; set; }
}