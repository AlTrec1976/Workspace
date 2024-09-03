using Workspace.Entities;

namespace Workspace.DAL;

public interface INoteRepository
{
    IAsyncEnumerable<WorkspaceNoteDTO> GetAllNotesAsync();
    Task<WorkspaceNoteDTO> GetByIDAsync(Guid id);
    Task UpdateAsync(WorkspaceNoteDTO workspaceNoteDto);
    Task<WorkspaceNoteDTO> CreateAsync(WorkspaceTaskDTO workspaceTaskDTO, WorkspaceNoteDTO workspaceNoteDto);
    Task DeleteAsync(Guid id);
}
