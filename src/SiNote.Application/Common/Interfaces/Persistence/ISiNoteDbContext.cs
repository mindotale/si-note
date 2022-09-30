using Microsoft.EntityFrameworkCore;

namespace SiNote.Application.Common.Interfaces.Persistence;

public interface ISiNoteDbContext
{
    DbSet<User> Users { get; set; }
    DbSet<Note> Notes { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
