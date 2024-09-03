using Workspace.Entities;

namespace Workspace.BLL.Logic.Contracts;

public interface ITaskService
{
    IAsyncEnumerable<WorkspaceTaskResponse> GetAllAsync();
    Task<WorkspaceTaskResponse?> GetByIdAsync(Guid id);
    Task UpdateAsync(Guid id, WorkspaceTaskRequest workspaceTaskRequest);
    Task CreateAsync(WorkspaceTaskRequest workspaceTaskRequest);
    Task DeleteAsync(Guid id);
    Task<List<WorkspaceTaskResponse>> GetAllTasksForMartAsync(Guid martId);
    Task SetEmployeeAsync(WorkspaceTaskEmployee workspaceTaskEmployee);
    Task ChangeStatus(TaskUserRequest taskUserRequest);
}