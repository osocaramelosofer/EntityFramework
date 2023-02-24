using API.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class DatabaseContext : IdentityDbContext<AppUser>
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public DbSet<MachineStatusCatalog> MachineStatusCatalog { get; set; }
        public DbSet<SYS_USER_DATA> SYS_USER_DATA { get; set; }
        public DbSet<AppUser> AppUser { get; set; }

    }
}
