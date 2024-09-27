using AppUser.Infrastructure.DomainUser.Configurations;
using AppUser.Infrastructure.DomainUser.ReadModels;
using Microsoft.EntityFrameworkCore;

namespace AppUser.Infrastructure.DomainUser.Contexts
{
    public sealed class UserReadDbContext : DbContext
    {
        public UserReadDbContext(DbContextOptions<UserReadDbContext> options): base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("AppUser");

            var configuration = new ReadConfiguration();
            modelBuilder.ApplyConfiguration<UserReadModel>(configuration);
            modelBuilder.ApplyConfiguration<AchievmentReadModel>(configuration);
            modelBuilder.ApplyConfiguration<UserAchievmentReadModel>(configuration);
            modelBuilder.ApplyConfiguration<ActivityStatusReadModel>(configuration);
        }
    }
}
