
using Workspace.Entities.Contracts;

namespace Workspace.Entities
{
    public class WorkspaceMart
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        //Тот, кто создал воркспейс, является его владельцем
        public IWorkspaceUser Owner { get; set; }
        //задания конкретного воркспейса
        public IEnumerable<WorkspaceTask?> Tasks { get; set; }
    }
}
