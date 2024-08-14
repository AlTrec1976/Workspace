using Microsoft.AspNetCore.Authorization;
using System.Security;
using System.Text;

namespace Workspace.Entities;

public class HasPermissionAttribute() : AuthorizeAttribute()
{
    public HasPermissionAttribute(Permission[] permission) :this()
    {
        base.Policy = ConvertArrayToString(ref permission);
    }

    private string ConvertArrayToString(ref Permission[] permission)
    {
        StringBuilder sb = new StringBuilder();

        foreach (Permission permissionItem in permission)
        {
            sb.Append("," + permissionItem.ToString());
        }

        sb.Remove(0, 1);
        return sb.ToString();
    }
}