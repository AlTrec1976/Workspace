using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Data;
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
        try
        {
            var sql = "SELECT * FROM public.get_all_notes()";

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
        try
        {
            var sql = "SELECT * FROM public.get_note(@id)";

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
        try
        {
            var sql = "CALL public.update_note(@id, @note, @userid)";

            var param = new
            {
                id = workspaceNoteDTO.Id,
                note = workspaceNoteDTO.Note,
                userid = workspaceNoteDTO.UserId,
            };

            await ExecuteAsync(sql, param);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при обновлении задания");
            throw;
        }
    }

    public async Task<WorkspaceNoteDTO> CreateAsync(WorkspaceTaskDTO workspaceTaskDTO, WorkspaceNoteDTO workspaceNoteDTO)
    {
        try
        {
            using var connection = GetConnection();

            var sql = "public.create_note";
            var param = new DynamicParameters();
            param.Add("@task_id", workspaceTaskDTO.Id);
            param.Add("@note_text", workspaceNoteDTO.Note);
            param.Add("@note_user", workspaceNoteDTO.UserId);
            param.Add("@note_id", dbType: DbType.Guid,
                                  direction: ParameterDirection.Output);

            await connection.ExecuteAsync(sql, param, commandType: CommandType.StoredProcedure);

            workspaceNoteDTO.Id = param.Get<Guid>("@note_id");

            return workspaceNoteDTO;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при создании задания");
            throw;
        }
    }

    public async Task DeleteAsync(Guid noteId)
    {
        try
        {
            var sql = "CALL public.delete_note(@id)";

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
