using Microsoft.AspNetCore.Authorization;
using System.Text;

namespace Workspace.Entities;

public class HasPermissionAttribute() : AuthorizeAttribute()
{
    public HasPermissionAttribute(Permission[] permission) :this()
    {
        StringBuilder sb = new StringBuilder();

        foreach (Permission permissionItem in permission) 
        { 
            sb.Append("," + permissionItem.ToString());    
        }

        sb.Remove(0, 1);
        base.Policy = sb.ToString();
    }
}