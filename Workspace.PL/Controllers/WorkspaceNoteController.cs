using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using Workspace.BLL.Logic.Contracts;
using Workspace.Entities;

namespace Workspace.PL;

[Route("api/[controller]")]
[ApiController]

public class WorkspaceNoteController(INoteService noteService, ILogger<WorkspaceNoteController> logger) : ControllerBase
{
    private readonly INoteService _noteService = noteService;
    private readonly ILogger _logger = logger;

    [Authorize]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<WorkspaceNoteResponse>>> GetAsync()
    {
        try
        {
            var _workspaceNotesResponse = await _noteService.GetAllAsync();
            return Ok(_workspaceNotesResponse);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка в GetAllAsync");
            throw;
        }
    }

    [Authorize]
    [HttpGet("{id}")]
    public async Task<ActionResult<WorkspaceNoteResponse>> GetAsync(Guid id)
    {
        try
        {
            var _WorkspaceNoteResponse = await _noteService.GetByIdAsync(id);

            if (_WorkspaceNoteResponse is null)
                return NotFound();

            return Ok(_WorkspaceNoteResponse);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка в GetAsync по id");
            throw;
        }
    }

    [Authorize]
    [HttpPut("{id}")]
    public async Task UpdateAsync(Guid id, [FromBody] WorkspaceNoteRequest WorkspaceNoteRequest)
    {
        try
        {
            var validator = new NoteValidator();
            var validationResult = validator.Validate(WorkspaceNoteRequest);

            if (!validationResult.IsValid)
            {
                var error = string.Empty;

                foreach (var item in validationResult.Errors)
                {
                    error += $"{item} \n";
                }

                throw new Exception(error);
            }

            await _noteService.UpdateAsync(id, WorkspaceNoteRequest);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка в UpdateAsync");
            throw;
        }
    }


    [Authorize]
    [HttpPost]
    public async Task CreateAsync([FromBody] WorkspaceNoteRequest WorkspaceNoteRequest)
    {
        try
        {
            var validator = new NoteValidator();
            var validationResult = validator.Validate(WorkspaceNoteRequest);

            if (!validationResult.IsValid)
            {
                var error = string.Empty;

                foreach (var item in validationResult.Errors)
                {
                    error += $"{item} \n";
                }

                throw new Exception(error);
            }

            await _noteService.CreateAsync(WorkspaceNoteRequest);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка в CreateAsync");
            throw;
        }
    }

    [Authorize]
    [HttpDelete("{id}")]
    public async Task DeleteAsync(Guid id)
    {
        try
        {
            await _noteService.DeleteAsync(id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка в DeleteAsync");
            throw;
        }
    }
}