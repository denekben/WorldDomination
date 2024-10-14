using User.Domain.Entities;
using User.Domain.Entities.Relationships;
using Microsoft.EntityFrameworkCore;
using User.Infrastructure.Configurations;

namespace User.Infrastructure.Contexts
{
    public sealed class UserWriteDbContext : DbContext
    {
        public DbSet<DomainUser> Users { get; set; }
        public DbSet<Achievment> Achievments { get; set; }
        public DbSet<UserAchievment> UserAchievments { get; set; }
        public DbSet<UserStatus> UserStatuses { get; set; }

        public UserWriteDbContext(DbContextOptions<UserWriteDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("User");

            var configuration = new WriteConfiguration();
            modelBuilder.ApplyConfiguration<DomainUser>(configuration);
            modelBuilder.ApplyConfiguration<Achievment>(configuration);
            modelBuilder.ApplyConfiguration<UserAchievment>(configuration);
            modelBuilder.ApplyConfiguration<UserStatus>(configuration);
        }
    }
}
