using Microsoft.EntityFrameworkCore;
using WorldDomination.Shared.Domain;
using User.Infrastructure.ReadModels;
using User.Infrastructure.Configurations;

namespace User.Infrastructure.Contexts
{
    public sealed class UserReadDbContext : DbContext
    {
        public DbSet<UserReadModel> Users { get; set; }

        public UserReadDbContext(DbContextOptions<UserReadDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("User");

            var configuration = new ReadConfiguration();
            modelBuilder.ApplyConfiguration<UserReadModel>(configuration);
            modelBuilder.ApplyConfiguration<AchievmentReadModel>(configuration);
            modelBuilder.ApplyConfiguration<UserAchievmentReadModel>(configuration);
            modelBuilder.ApplyConfiguration<UserStatusReadModel>(configuration);
        }
    }
}
