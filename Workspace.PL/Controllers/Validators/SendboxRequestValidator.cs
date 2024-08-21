using FluentValidation;
using Workspace.Entities;

namespace Workspace.PL;

public class SendboxRequestValidator : AbstractValidator<SendboxRequest>
{
    public SendboxRequestValidator()
    {
        RuleFor(u => u.InviteId)
            .NotNull()
            .NotEmpty();

        RuleFor(u => u.UserId)
            .NotNull()
            .NotEmpty();

        RuleFor(u => u.MartId)
            .NotNull()
            .NotEmpty();
    }
}
