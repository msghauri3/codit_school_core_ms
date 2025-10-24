using Microsoft.EntityFrameworkCore;
using codit_school_core_ms.Models;

namespace codit_school_core_ms.Models
{
    public class SchoolContext : DbContext
    {
        public SchoolContext(DbContextOptions<SchoolContext> options)
            : base(options)
        {
        }

        public DbSet<Login> Logins { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Login>()
                .HasIndex(l => l.Username)
                .IsUnique();

            modelBuilder.Entity<Login>()
                .Property(l => l.CreatedDate)
                .HasDefaultValueSql("GETDATE()");
        }
    }
}
