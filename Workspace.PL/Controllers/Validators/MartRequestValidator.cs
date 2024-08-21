using FluentValidation;
using Workspace.Entities;

namespace Workspace.PL;

public class MartRequestValidator : AbstractValidator<WorkspaceMartRequest>
{
    public MartRequestValidator()
    {
        RuleFor(u => u.Name)
            .NotNull()
            .NotEmpty();
    }
}
