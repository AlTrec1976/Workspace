using Workspace.DAL;
using Workspace.Entities;

namespace Workspace.BLL.Logic.Contracts;

public interface ITaskService
{
    Task<List<WorkspaceTaskResponse>> GetAllAsync();
    Task<WorkspaceTaskResponse?> GetByIdAsync(Guid id);
    Task UpdateAsync(Guid id, WorkspaceTaskRequest workspaceTaskRequest);
    Task CreateAsync(WorkspaceTaskRequest workspaceTaskRequest);
    Task DeleteAsync(Guid id);
    Task<List<WorkspaceTaskResponse>> GetAllTasksForMartAsync(Guid martId);
    Task SetEmployeeAsync(Guid id, WorkspaceTaskRequest workspaceTaskRequest);
}