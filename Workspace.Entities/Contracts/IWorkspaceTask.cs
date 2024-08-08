namespace Workspace.Entities.Contracts;

public interface IWorkspaceTask
{
    public Guid Id { get; set; }

    public string Name { get; set; }
    public StatusTask Status { get; set; }
    public IEnumerable<IWorkspaceNote> Notes { get; set; }
    public Guid ManagerId { get; set; }
    public Guid? EmployeeId { get; set; }
}