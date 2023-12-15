using Balance.Domain.Entities;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Balance.Infrastructure.Data
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {

        }

        public DbSet<Euro> Euro { get; set; }

        public DbSet<AccountBank> AccountBanks { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            SetDefaultProperties();
            return base.SaveChangesAsync(cancellationToken);
        }

        public override int SaveChanges()
        {
            SetDefaultProperties();
            return base.SaveChanges();
        }

        private void SetDefaultProperties()
        {
            foreach (var auditableEntity in ChangeTracker.Entries<BaseEntity>())
            {
                if (auditableEntity.State == EntityState.Added)
                {
                    if (!auditableEntity.Entity.CreatedAt.HasValue)
                    {
                        auditableEntity.Entity.CreatedAt = DateTime.Now;
                    }
                }
                else
                {
                    auditableEntity.Property(p => p.CreatedAt).IsModified = false;
                }
            }
        }
    }
}
