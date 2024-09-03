
namespace Workspace.Entities
{
    public class WorkspaceTaskEmployee
    {
        public Guid TaskID { get; set; } = Guid.Empty;
        public Guid EmployeeId { get; set; } = Guid.Empty;
    }
}
