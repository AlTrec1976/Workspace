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
