using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workspace.Entities.Contracts;

namespace Workspace.Entities;

    public interface IOrgRole
    {
        public IWorkspaceUser User { get; }
        public IWorkspaceTask Task { get; }
        public void ChangeStatus(StatusTask statusTask);
    }