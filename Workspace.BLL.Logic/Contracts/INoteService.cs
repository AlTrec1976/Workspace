using Workspace.Entities;
using Workspace.Entities.Contracts;

namespace Workspace.BLL.Logic.Contracts;

public interface INoteService
{
    Task<List<WorkspaceNoteResponse>> GetAllAsync();
    Task<WorkspaceNoteResponse?> GetByIdAsync(Guid id);
    Task UpdateAsync(Guid id, WorkspaceNoteRequest workspaceNoteRequest);
    Task CreateAsync(WorkspaceNoteRequest workspaceNoteRequest);
    Task DeleteAsync(Guid id);
}