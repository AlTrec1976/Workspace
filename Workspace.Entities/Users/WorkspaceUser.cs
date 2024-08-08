﻿using Workspace.Entities.Contracts;

namespace Workspace.Entities;

public class WorkspaceUser : IWorkspaceUser
{
    public Guid Id { get; set; }

    public string Login { get; set; }
    public string Password { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }

    public string? Status { get; set; }
}