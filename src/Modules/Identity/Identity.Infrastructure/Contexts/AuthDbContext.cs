using Identity.Infrastructure.Configurations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Identity.Infrastructure.Models;

namespace Identity.Infrastructure.Contexts
{
    public sealed class AuthDbContext : IdentityDbContext<AuthUser>
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("Identity");

            var configuration = new IdentityConfiguration();
            modelBuilder.ApplyConfiguration<IdentityRole>(configuration);
            base.OnModelCreating(modelBuilder);
        }
    }
}
