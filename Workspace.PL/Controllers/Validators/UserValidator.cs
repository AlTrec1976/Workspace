using FluentValidation;
using Workspace.Entities;

namespace Workspace.PL;

public class UserValidator : AbstractValidator<WorkspaceUserRequest>
{
    public UserValidator()
    {
        RuleFor(u => u.Login)
            .NotNull()
            .NotEmpty()
            .Length(3, 20);

        RuleFor(u => u.Password)
            .NotNull()
            .NotEmpty()
            .Length(5, 128);

        RuleFor(u => u.Name)
            .NotNull()
            .NotEmpty()
            .Length(2, 20);

        RuleFor(u => u.Surname)
            .NotNull()
            .NotEmpty()
            .Length(2,20);
    }
}
