using Workspace.BLL.Logic.Contracts;
using Workspace.Entities.Contracts;

namespace Workspace.BLL.Logic;

public class Workspace
{
    private IUserContext _context;
    private IWorkspaceTask _task;
    public Workspace(IWorkspaceTask task, IUserContext userContext)
    {
        _context = userContext;
        _task = task;
    }

    public void ChangeStatus()
    {
        _context.ChangeStatus(_task);
    }
}
/*Новый пользователь
 *
 *
 *
 *
 * 
 */

/*
 * Принял приглашение
 */


/*
 *  Workspace 01
 * Workspace 02 ты создавал, значит ты менеджер
 */

/*
 * Захожу и только в этом момент система определяет меня как сотрудника
 */
