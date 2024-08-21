using FluentValidation;
using Workspace.Entities;

namespace Workspace.PL
{
    public class RoleRequestValidator : AbstractValidator<RoleRequest>
    {
        public RoleRequestValidator()
        {
            RuleFor(u => u.RoleName)
                .NotNull()
                .NotEmpty(); 
        }
    }
}
