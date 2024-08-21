using FluentValidation;
using Workspace.Entities;

namespace Workspace.PL;

public class NoteRequestValidator : AbstractValidator<WorkspaceNoteRequest>
{
    public NoteRequestValidator()
    {
        RuleFor(u => u.Note)
            .NotNull()
            .NotEmpty()
            .MinimumLength(6);

        RuleFor(u => u.UserId)
            .NotNull()
            .NotEmpty();
    }
}
