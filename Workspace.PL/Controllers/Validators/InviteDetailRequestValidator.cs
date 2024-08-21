using FluentValidation;
using Workspace.Entities;

namespace Workspace.PL;

public class InviteDetailRequestValidator : AbstractValidator<InviteDetailRequest>
{
    public InviteDetailRequestValidator()
    {
        RuleFor(u => u.InviteID)
            .NotNull()
            .NotEmpty();

        RuleFor(u => u.UserID)
            .NotNull()
            .NotEmpty();

        RuleFor(u => u.Comments)
            .NotNull()
            .NotEmpty();
    }
}
