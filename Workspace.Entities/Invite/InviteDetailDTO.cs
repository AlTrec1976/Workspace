using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workspace.Entities
{
    public class InviteDetailDTO
    {
        public Guid InviteID { get; set; }
        public Guid UserID { get; set; }
        public string Comments { get; set; }
    }
}
