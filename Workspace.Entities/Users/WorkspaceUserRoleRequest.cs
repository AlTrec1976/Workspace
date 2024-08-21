using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workspace.Entities
{
    public class WorkspaceUserRoleRequest
    {
        public Guid Id { get; set; }
        public int RoleId { get; set; }
    }
}
