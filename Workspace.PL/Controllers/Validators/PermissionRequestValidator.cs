using FluentValidation;
using Workspace.Entities;

namespace Workspace.PL
{
    public class PermissionRequestValidator : AbstractValidator<WorkspacePermissionRequest>
    {
        public PermissionRequestValidator()
        {
            RuleFor(u => u.PermissionName)
                    .NotNull()
                    .NotEmpty();
        }
    }
}
