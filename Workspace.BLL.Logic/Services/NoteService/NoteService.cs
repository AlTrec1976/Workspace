using AutoMapper;
using Microsoft.Extensions.Logging;

using Workspace.BLL.Logic.Contracts;
using Workspace.DAL;
using Workspace.Entities;

namespace Workspace.BLL.Logic;

public class NoteService(IMapper mapper, INoteRepository noteRepository, ILogger<NoteService> logger) : INoteService
{

    private readonly INoteRepository _noteRepository = noteRepository;
    private readonly IMapper _mapper = mapper;
    private readonly ILogger _logger = logger;

    public async IAsyncEnumerable<WorkspaceNoteResponse> GetAllAsync()
    {
        await foreach (var item in _noteRepository.GetAllNotesAsync())
        {
            yield return _mapper.Map<WorkspaceNoteResponse>(item);
        }
    }

    public async Task<WorkspaceNoteResponse?> GetByIdAsync(Guid id)
    {
        try
        {
            var workspaceNotes = new WorkspaceNote();
            var workspaceNotesResponse = new WorkspaceNoteResponse();

            var workspaceNotesDTO = await _noteRepository.GetByIDAsync(id);

            workspaceNotes = _mapper.Map<WorkspaceNote>(workspaceNotesDTO);
            workspaceNotesResponse = _mapper.Map<WorkspaceNoteResponse>(workspaceNotes);

            return workspaceNotesResponse;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка в GetByIdAsync");
            throw;
        }
    }

    public async Task UpdateAsync(Guid id, WorkspaceNoteRequest workspaceNoteRequest)
    {
        try
        {
            var _workspaceNote = new WorkspaceNote();
            var _workspaceNoteDTO = new WorkspaceNoteDTO();

            _workspaceNote = _mapper.Map<WorkspaceNote>(workspaceNoteRequest);
            _workspaceNote.Id = id;

            _workspaceNoteDTO = _mapper.Map<WorkspaceNoteDTO>(_workspaceNote);

            await _noteRepository.UpdateAsync(_workspaceNoteDTO);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка в UpdateAsync");
            throw;
        }
    }

    public async Task<WorkspaceNoteResponse> CreateAsync(Guid id, WorkspaceNoteRequest workspaceNoteRequest)
    {
        try
        {
            var _taskDTO = new WorkspaceTaskDTO()
            {
                Id = id,
            };

            var _workspaceNote = new WorkspaceNote();
            _workspaceNote = _mapper.Map<WorkspaceNote>(workspaceNoteRequest);
            var _workspaceNoteDTO = _mapper.Map<WorkspaceNoteDTO>(_workspaceNote);

            _workspaceNoteDTO = await _noteRepository.CreateAsync(_taskDTO, _workspaceNoteDTO);

            var _workspaceNoteResponse = _mapper.Map<WorkspaceNoteResponse>(_workspaceNoteDTO);
            return _workspaceNoteResponse;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка в CreateAsync");
            throw;
        }
    }
    public async Task DeleteAsync(Guid id)
    {
        try
        {
            await _noteRepository.DeleteAsync(id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка в DeleteAsync");
            throw;
        }
    }
}
