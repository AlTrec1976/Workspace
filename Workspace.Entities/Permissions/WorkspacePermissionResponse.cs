using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workspace.Entities
{
    public class WorkspacePermissionResponse : WorkspacePermissionRequest
    {
        public int PermissionId { get; set; }
    }
}
