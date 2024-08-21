using FluentValidation;
using Workspace.Entities;

namespace Workspace.PL
{
    public class RoleResponseValidator : AbstractValidator<RoleResponse>
    {
        public RoleResponseValidator()
        {
            RuleFor(u => u.RoleName)
                .NotNull()
                .NotEmpty(); 
        }
    }
}
