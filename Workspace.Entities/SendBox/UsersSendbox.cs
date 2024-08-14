using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workspace.Entities.Contracts;

namespace Workspace.Entities.Workspaces
{
    //песочница юзеров, которых отобрали из приглашения
    public class UsersSendbox
    {
        //воркспейс
        public WorkspaceMart workspaceMart { get; set; }
        //приглашенные пользователи
        public IEnumerable<IWorkspaceUser?> Users { get; set; }
    }
}
