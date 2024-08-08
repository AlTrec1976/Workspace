using Microsoft.EntityFrameworkCore;
using Workspace.Entities;

namespace Workspace.DAL;

public class AppDbContext  : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    public DbSet<WorkspaceTaskDTO> Tasks { get; set; }
    public DbSet<WorkspaceUserDTO> Users { get; set; }
    public DbSet<WorkspaceNoteDTO> Notes { get; set; }
}