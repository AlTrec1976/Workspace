using System.ComponentModel.DataAnnotations;
using Workspace.Entities.Contracts;

namespace Workspace.Entities;

public class WorkspaceTaskRequest
{
    public string Name { get; set; }
    public int Status { get; set; }
    public Guid? ManagerId { get; set; }
    public Guid? EmployeeId { get; set; }
}