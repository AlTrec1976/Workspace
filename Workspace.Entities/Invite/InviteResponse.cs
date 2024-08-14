

namespace Workspace.Entities
{
    public class InviteResponse : InviteRequest
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string MartName { get; set; }
        public string UserName { get; set; }
    }
}
