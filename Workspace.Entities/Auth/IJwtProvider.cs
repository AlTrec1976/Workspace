using Workspace.Entities;

namespace Workspace.Auth;

public interface IJwtProvider
{
    string GenerateToken(WorkspaceUser workspaceUser);
}