using AppUser.Domain.Entities;
using AppUser.Domain.Entities.Relationships;
using AppUser.Infrastructure.DomainUser.Configurations;
using Microsoft.EntityFrameworkCore;
using UserAccess.Domain.Entities;

namespace AppUser.Infrastructure.DomainUser.Contexts
{
    public sealed class UserWriteDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Achievment> Achievments { get; set; }
        public DbSet<UserAchievment> UserAchievments { get; set; }
        public DbSet<ActivityStatus> ActivityStatuses { get; set; }

        public UserWriteDbContext(DbContextOptions<UserWriteDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("AppUser");

            var configuration = new WriteConfiguration();
            modelBuilder.ApplyConfiguration<User>(configuration);
            modelBuilder.ApplyConfiguration<Achievment>(configuration);
            modelBuilder.ApplyConfiguration<UserAchievment>(configuration);
            modelBuilder.ApplyConfiguration<ActivityStatus>(configuration);
        }
    }
}
