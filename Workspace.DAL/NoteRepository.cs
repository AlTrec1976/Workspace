using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Workspace.Entities;

namespace Workspace.DAL;

public class NoteRepository : BaseRepository, INoteRepository
{
    private readonly ILogger _logger;

    public NoteRepository(ILogger<TaskRepository> logger, IConfiguration configuration)
        : base(logger, configuration)
    {
        _logger = logger;
    }

    public async Task<IEnumerable<WorkspaceNoteDTO>> GetAllNotesAsync()
    {
        var sql = "SELECT * FROM public.get_all_notes()";

        try
        {
            return await QueryAsync<WorkspaceNoteDTO>(sql);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при выводе списка заданий");
            throw;
        }
    }

    public async Task<WorkspaceNoteDTO> GetByIDAsync(Guid noteId)
    {
        var sql = "SELECT * FROM public.get_note(@id)";

        try
        {
            var param = new { id = noteId };
            return await QuerySingleAsync<WorkspaceNoteDTO>(sql, param);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при поиска задания по Id");
            throw;
        }
    }

    public async Task UpdateAsync(WorkspaceNoteDTO workspaceNoteDTO)
    {
        var sql = "CALL public.update_note(@id, @note, @userid, @taskid)";

        try
        {
            var param = new
            {
                id = workspaceNoteDTO.Id,
                note = workspaceNoteDTO.Note,
                userid = workspaceNoteDTO.UserId,
                taskid = workspaceNoteDTO.TaskId
            };

            await ExecuteAsync(sql, param);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при обновлении задания");
            throw;
        }
    }

    public async Task CreateAsync(WorkspaceNoteDTO workspaceNoteDTO)
    {
        var sql = "CALL public.create_note(@note, @userid, @taskid)";

        try
        {
            var param = new
            {
                note = workspaceNoteDTO.Note,
                userid = workspaceNoteDTO.UserId,
                taskid = workspaceNoteDTO.TaskId
            };

            await ExecuteAsync(sql, param);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при создании задания");
            throw;
        }
    }

    public async Task DeleteAsync(Guid noteId)
    {
        var sql = "CALL public.delete_note(@id)";

        try
        {
            var param = new { id = noteId };
            await ExecuteAsync(sql, param);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при удалении задания");
            throw;
        }
    }
}
