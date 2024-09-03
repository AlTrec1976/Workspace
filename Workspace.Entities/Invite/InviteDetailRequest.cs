namespace Workspace.Entities
{
    public class InviteDetailRequest
    {
        public Guid InviteID { get; set; }
        public Guid UserID { get; set; }
        public string Comments { get; set; }
    }
}
