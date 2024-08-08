using Workspace.Entities.Contracts;

namespace Workspace.BLL.Logic.Contracts;

public interface IUserContext
{
    public void ChangeStatus(IWorkspaceTask task);
}
