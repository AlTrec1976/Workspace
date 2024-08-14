using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workspace.Entities
{
    public class InviteDTO
    {
        public Guid Id { get; set; }
        public string InviteText { get; set; }
        public bool IsOppened { get; set; }
        public Guid MartId { get; set; }
        public Guid UserId { get; set; }
    }
}
