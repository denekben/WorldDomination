using Game.Infrastructure.Configurations;
using Game.Domain.DomainModels.ReadModels.GameAggregate;
using Game.Domain.DomainModels.ReadModels.RoomAggregate;
using Game.Domain.DomainModels.ReadModels.UserAggregate;
using Microsoft.EntityFrameworkCore;
using Game.Infrastructure.Seed;

namespace Game.Infrastructure.Contexts
{
    public sealed class GameReadDbContext : DbContext
    {
        public DbSet<RoomReadModel> Rooms { get; set; }
        public DbSet<CountryPatternReadModel> CountryPatterns { get; set; }
        public DbSet<CityPatternReadModel> CityPatterns { get; set; }

        public GameReadDbContext(DbContextOptions<GameReadDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("Game");

            var configuration = new ReadConfiguration();

            modelBuilder.ApplyConfiguration<CityReadModel>(configuration);
            modelBuilder.ApplyConfiguration<CountryReadModel>(configuration);

            modelBuilder.ApplyConfiguration<GameReadModel>(configuration);

            //modelBuilder.ApplyConfiguration<OrganizerReadModel>(configuration);
            //modelBuilder.ApplyConfiguration<PlayerReadModel>(configuration);
            modelBuilder.ApplyConfiguration<RoomMemberReadModel>(configuration);
            modelBuilder.ApplyConfiguration<RoomReadModel>(configuration);

            modelBuilder.ApplyConfiguration<GameUserReadModel>(configuration);

            modelBuilder.ApplyConfiguration<CityPatternReadModel>(configuration);
            modelBuilder.ApplyConfiguration<CountryPatternReadModel>(configuration);
        }
    }
}
