using AppUser.Infrastructure.Identity.Configurations;
using AppUser.Infrastructure.Identity.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using AppUser.Domain.Entities.Relationships;
using AppUser.Domain.Entities;
using AppUser.Infrastructure.DomainUser.Configurations;
using UserAccess.Domain.Entities;

namespace AppUser.Infrastructure.Identity.Contexts
{
    public sealed class AuthDbContext : IdentityDbContext<AuthUser>
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("AppUser");

            var configuration = new IdentityConfiguration();
            modelBuilder.ApplyConfiguration<IdentityRole>(configuration);
            base.OnModelCreating(modelBuilder);
        }
    }
}
