namespace Workspace.Entities
{
    public class Role
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public IEnumerable<WorkspacePermission> Permissions { get; set; }
    }
}
