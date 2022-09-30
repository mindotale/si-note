using Microsoft.EntityFrameworkCore;

namespace SiNote.Infrastructure.Persistence;

public class SiNoteDbContextInitializer
{
    public static void Initialize(SiNoteDbContext context)
    {
        context.Database.EnsureCreated();
    }
}
