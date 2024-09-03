namespace Workspace.Entities.Contracts;

public interface IWorkspaceUser
{
    public Guid Id { get; set; }

    public string Login { get; set; }
    public string Password { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
}
