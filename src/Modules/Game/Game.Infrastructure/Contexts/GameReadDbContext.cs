using Game.Infrastructure.Configurations;
using Game.Infrastructure.ReadModels.CountryAggregate;
using Game.Infrastructure.ReadModels.GameAggregate;
using Game.Infrastructure.ReadModels.RoomAggregate;
using Game.Infrastructure.ReadModels.UserAggregate;
using Microsoft.EntityFrameworkCore;

namespace Game.Infrastructure.Contexts
{
    public sealed class GameReadDbContext : DbContext
    {
        public GameReadDbContext(DbContextOptions<GameReadDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var configuration = new ReadConfiguration();

            modelBuilder.ApplyConfiguration<CityReadModel>(configuration);
            modelBuilder.ApplyConfiguration<CountryReadModel>(configuration);

            modelBuilder.ApplyConfiguration<GameReadModel>(configuration);

            modelBuilder.ApplyConfiguration<OrganizerReadModel>(configuration);
            modelBuilder.ApplyConfiguration<PlayerReadModel>(configuration);
            modelBuilder.ApplyConfiguration<RoomMemberReadModel>(configuration);
            modelBuilder.ApplyConfiguration<RoomReadModel>(configuration);

            modelBuilder.ApplyConfiguration<GameUserReadModel>(configuration);
        }
    }
}
