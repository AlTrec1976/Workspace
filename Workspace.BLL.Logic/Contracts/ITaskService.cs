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
    void ChangeStatus();
}