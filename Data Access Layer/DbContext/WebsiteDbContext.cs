using Microsoft.EntityFrameworkCore;

using VelascoPersonalWebsite_IPT.DataAccess.Entities;

namespace VelascoPersonalWebsite_IPT.DataAccess.DbContext
{
    public class WebsiteDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public WebsiteDbContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<UserAccount> UserAccounts { get; set; }

        public DbSet<AuditLog> AuditLogs { get; set; }

        public DbSet<Profile> Profiles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(WebsiteDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }

    }
}
