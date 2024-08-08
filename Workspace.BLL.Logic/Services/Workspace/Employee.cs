using Workspace.Entities;
using Workspace.Entities.Contracts;

namespace Workspace.BLL.Logic;

public class Employee : WorkspaceUser
{
    public void ChangeStatus(IWorkspaceTask task)
    {
        //task.ChangeStatus();
        //здесь алгоритм как устанавливает статус сотрудник
        Console.WriteLine("Я сотрудник!");
    }
}
