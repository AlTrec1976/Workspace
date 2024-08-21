using FluentValidation;
using Workspace.Entities;

namespace Workspace.PL;

public class TaskShortRequestValidator : AbstractValidator<WorkspaceTaskShortRequest>
{
    public TaskShortRequestValidator()
    {
        RuleFor(u => u.Name)
            .NotNull()
            .NotEmpty();
    }
}
