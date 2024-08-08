using Workspace.Entities;

namespace Workspace.DAL;

public interface ITaskRepository
{
    Task<IEnumerable<WorkspaceTaskDTO>> GetAllTasksAsync();
    Task<WorkspaceTaskDTO> GetByIDAsync(Guid id);
    Task UpdateAsync(WorkspaceTaskDTO workspaceTaskDto);
    Task CreateAsync(WorkspaceTaskDTO workspaceTaskDto);
    Task DeleteAsync(Guid id);
}
