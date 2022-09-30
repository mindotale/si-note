using CleanArchitecture.Infrastructure.Persistence.Interceptors;
using Microsoft.EntityFrameworkCore;
using SiNote.Application.Common.Interfaces.Persistence;
using SiNote.Domain.Entities;
using SiNote.Infrastructure.Persistence.Configurations;
using System.Reflection;

namespace SiNote.Infrastructure.Persistence
{
    public class SiNoteDbContext : DbContext, ISiNoteDbContext
    {
        private readonly AuditableEntitySaveChangesInterceptor _auditableEntitySaveChangesInterceptor;

        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Note> Notes { get; set; } = null!;

        public SiNoteDbContext(
            DbContextOptions<SiNoteDbContext> options,
            AuditableEntitySaveChangesInterceptor auditableEntitySaveChangesInterceptor
            )
            : base(options) 
        {
            _auditableEntitySaveChangesInterceptor = auditableEntitySaveChangesInterceptor;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new UserConfiguration());
            builder.ApplyConfiguration(new NoteConfiguration());

            base.OnModelCreating(builder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.AddInterceptors(_auditableEntitySaveChangesInterceptor);
        }
    }
}
