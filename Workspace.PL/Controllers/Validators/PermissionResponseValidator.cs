using FluentValidation;
using Workspace.Entities;

namespace Workspace.PL;

public class PermissionResponseValidator : AbstractValidator<WorkspacePermissionResponse>
    {
        public PermissionResponseValidator()
        {
        RuleFor(u => u.PermissionName)
                .NotNull()
                .NotEmpty();
    }
}

