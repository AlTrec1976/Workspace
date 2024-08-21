using FluentValidation;
using Workspace.Entities;

namespace Workspace.PL;

public class InviteRequestValidator : AbstractValidator<InviteRequest>
{
    public InviteRequestValidator()
    {
        RuleFor(u => u.InviteText)
            .NotNull()
            .NotEmpty();

        RuleFor(u => u.MartId)
            .NotNull()
            .NotEmpty();
    }
}
