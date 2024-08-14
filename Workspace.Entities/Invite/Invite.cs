
using Workspace.Entities.Contracts;

namespace Workspace.Entities;

public class Invite
{
    public Guid Id { get; set; }
    //данные о ворке с которого приглашение (по сути нужен ид ворка и ид овнера)
    public WorkspaceMart WorkspaceMartObj { get; set; }
    //некий текст приглашения
    public string InviteText { get; set; }
    //юзеры которые примут приглашения
    public IList<IWorkspaceUser> SubscribedUsers { get; set; }
    //истина если приглашение актуально
    public bool IsOppened { get; set; }
}
