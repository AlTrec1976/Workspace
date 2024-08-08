using AutoMapper;

using Workspace.BLL.Logic.Contracts;
using Workspace.DAL;
using Workspace.Entities;
using Microsoft.Extensions.Logging;

namespace Workspace.BLL.Logic;

public class NoteService(AppDbContext context, IMapper mapper, INoteRepository noteRepository, ILogger<NoteService> logger) : INoteService
{
    private readonly AppDbContext _context = context;
    private readonly INoteRepository _noteRepository = noteRepository;
    private readonly IMapper _mapper = mapper;
    private readonly ILogger _logger = logger;

    //TODO: Переделать маппинг только по MappingProfiles
    public async Task<List<WorkspaceNoteResponse>> GetAllAsync()
    {
        try
        {
            var workspaceNotes = new List<WorkspaceNoteResponse>();
            var workspaceNoteResponse = new List<WorkspaceNoteResponse>();

            var workspaceNoteResponsesDTO = await _noteRepository.GetAllNotesAsync();

            workspaceNotes = _mapper.Map<List<WorkspaceNoteResponse>>(workspaceNoteResponsesDTO);

            workspaceNoteResponse = _mapper.Map<List<WorkspaceNoteResponse>>(workspaceNotes);

            return workspaceNotes;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка в GetAllAsync");
            throw;
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

    public async Task CreateAsync(WorkspaceNoteRequest workspaceNoteRequest)
    {
        try
        {
            var _workspaceNote = new WorkspaceNote();
            var _workspaceNoteDTO = new WorkspaceNoteDTO();

            _workspaceNote = _mapper.Map<WorkspaceNote>(workspaceNoteRequest);
            _workspaceNoteDTO = _mapper.Map<WorkspaceNoteDTO>(_workspaceNote);

            await _noteRepository.CreateAsync(_workspaceNoteDTO);
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
