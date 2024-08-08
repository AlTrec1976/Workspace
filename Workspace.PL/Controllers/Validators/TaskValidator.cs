using FluentValidation;
using Workspace.Entities;

namespace Workspace.PL;

public class TaskValidator : AbstractValidator<WorkspaceTaskRequest>
{
    public TaskValidator()
    {
        RuleFor(u => u.Name)
            .NotNull()
            .NotEmpty()
            .Length(6, 180);
        RuleFor(u => u.Status)
            .InclusiveBetween(1, 7);
    }
}
