using Balance.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Balance.Infrastructure.Data
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {

        }

        public DbSet<Euro> Euro { get; set; }
    }
}
