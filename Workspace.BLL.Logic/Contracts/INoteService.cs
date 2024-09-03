using Workspace.Entities;


namespace Workspace.BLL.Logic.Contracts;

public interface INoteService
{
    IAsyncEnumerable<WorkspaceNoteResponse> GetAllAsync();
    Task<WorkspaceNoteResponse?> GetByIdAsync(Guid id);
    Task UpdateAsync(Guid id, WorkspaceNoteRequest workspaceNoteRequest);
    Task<WorkspaceNoteResponse> CreateAsync(Guid id, WorkspaceNoteRequest workspaceNoteRequest);
    Task DeleteAsync(Guid id);
}