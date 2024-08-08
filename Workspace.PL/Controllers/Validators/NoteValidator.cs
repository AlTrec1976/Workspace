using FluentValidation;
using Workspace.Entities;

namespace Workspace.PL;

public class NoteValidator : AbstractValidator<WorkspaceNoteRequest>
{
    public NoteValidator()
    {
        RuleFor(u => u.Note)
            .NotNull()
            .NotEmpty()
            .MinimumLength(6);
        RuleFor(u => u.UserId)
            .NotNull()
            .NotEmpty();
        RuleFor(u => u.TaskId)
            .NotNull()
            .NotEmpty();
    }
}
