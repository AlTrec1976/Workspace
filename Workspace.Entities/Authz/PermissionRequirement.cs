using Microsoft.AspNetCore.Authorization;

namespace Workspace.Entities;

public class PermissionRequirement : IAuthorizationRequirement
{
    public string policyName;
    public PermissionRequirement(Permission[] permissions)
    {
        Permissions = permissions;
    }
   public Permission[] Permissions { get; set; } = [];
}