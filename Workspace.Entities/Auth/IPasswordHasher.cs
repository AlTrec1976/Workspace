namespace Workspace.Auth;

public interface IWorkspacePasswordHasher
{
    string Generate(string password);
    bool Verify(string password, string hashedPassword);
}