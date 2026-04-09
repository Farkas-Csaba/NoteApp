using NoteApp.Identity;

namespace NoteApp.DbContext;
using Microsoft.EntityFrameworkCore;

public class NoteAppDbContext : DbContext
{
    public DbSet<Notes> Notes { get; set; }

    public NoteAppDbContext(DbContextOptions<NoteAppDbContext> options) : base(options)
    {
        
    }
}