using Workspace.Entities;

namespace Workspace.DAL;

public interface ITaskRepository
{
    IAsyncEnumerable<WorkspaceTaskDTO> GetAllTasksAsync();
    Task<WorkspaceTaskDTO> GetByIDAsync(Guid id);
    Task UpdateAsync(WorkspaceTaskDTO workspaceTaskDto);
    Task CreateAsync(WorkspaceTaskDTO workspaceTaskDto);
    Task DeleteAsync(Guid id);
    Task<IEnumerable<WorkspaceTaskDTO>> GetAllTasksForMartAsync(Guid martId);
    Task SetEmployeeAsync(WorkspaceTaskDTO workspaceTaskDTO);
    bool IsTaskOwner(WorkspaceTaskDTO task, WorkspaceUserDTO user);
    Task UpdateStatus(WorkspaceTaskDTO workspaceTaskDTO);
}
