

namespace Workspace.Entities
{
    public class InviteRollDTO
    {
        public Guid InviteId { get; set; }
        public Guid MartId { get; set; }
        public Guid UserId { get; set; }
        public bool IsOpened { get; set; }
        public string MartName { get; set; }
        public string InviteComments { get; set; }
        public string UserName { get; set; }
    }
}