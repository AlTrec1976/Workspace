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

    /// <summary>
    /// Запрос всех заметок
    /// </summary>
    [Authorize]
    [HasPermission([Permission.user, Permission.read])]
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

    /// <summary>
    /// Запрос заметок по ID
    /// </summary>
    [Authorize]
    [HasPermission([Permission.user, Permission.read])]
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

    /// <summary>
    /// Изменение заметки
    /// </summary>
    [Authorize]
    [HasPermission([Permission.user, Permission.update])]
    [HttpPut("{id}")]
    public async Task UpdateAsync(Guid id, [FromBody] WorkspaceNoteRequest WorkspaceNoteRequest)
    {
        try
        {
            var validator = new NoteRequestValidator();
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


    /// <summary>
    /// Создание заметки
    /// </summary>
    [Authorize]
    [HasPermission([Permission.user, Permission.create])]
    [HttpPost]
    public async Task<WorkspaceNoteResponse> CreateAsync(Guid id, [FromBody] WorkspaceNoteRequest WorkspaceNoteRequest)
    {
        try
        {
            var validator = new NoteRequestValidator();
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

            return await _noteService.CreateAsync(id, WorkspaceNoteRequest);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка в CreateAsync");
            throw;
        }
    }

    /// <summary>
    /// Удаление заметки
    /// </summary>
    [Authorize]
    [HasPermission([Permission.user, Permission.delete])]
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