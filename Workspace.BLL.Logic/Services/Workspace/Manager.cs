using Workspace.Entities;
using Workspace.Entities.Contracts;

namespace Workspace.BLL.Logic;

public class Manager : WorkspaceUser
{
    public void ChangeStatus(IWorkspaceTask task)
    {
        //здесь алгоритм как устанавливает статус мэнеджер
        //task.ChangeStatus();
        Console.WriteLine("Я менеджер!");
    }
}
