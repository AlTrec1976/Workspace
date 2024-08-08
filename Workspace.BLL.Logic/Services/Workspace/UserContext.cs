using Workspace.BLL.Logic.Contracts;
using Workspace.Entities.Contracts;

namespace Workspace.BLL.Logic;

public class UserContext : IUserContext
{
    private IWorkspaceUser _user;

    public UserContext(IWorkspaceUser user)
    {
        _user = user;
    }

    public void ChangeStatus(IWorkspaceTask task)
    {
        /*
         * если нельзя установить статус, то 
         */
        //_user.ChangeStatus(task);
        ///*статус не менять*/
        //task.ChangeStatus();

    }
}
