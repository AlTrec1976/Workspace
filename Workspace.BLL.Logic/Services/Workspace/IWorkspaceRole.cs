using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workspace.Entities;
using Workspace.Entities.Contracts;

namespace Workspace.BLL.Logic
{
    public interface IWorkspaceRole
    {
        IWorkspaceUser User { get; }
        IWorkspaceTask Task { get; }

        void ChangeRole();
        void ChangeStatus(IWorkspaceTask workspaceTask);
    }
}
