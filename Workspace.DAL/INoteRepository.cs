using Workspace.Entities;

namespace Workspace.DAL;

public interface INoteRepository
{
    Task<IEnumerable<WorkspaceNoteDTO>> GetAllNotesAsync();
    Task<WorkspaceNoteDTO> GetByIDAsync(Guid id);
    Task UpdateAsync(WorkspaceNoteDTO workspaceNoteDto);
    Task CreateAsync(WorkspaceNoteDTO workspaceNoteDto);
    Task DeleteAsync(Guid id);
}
