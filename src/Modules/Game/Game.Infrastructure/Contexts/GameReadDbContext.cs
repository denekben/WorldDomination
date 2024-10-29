using Game.Infrastructure.Configurations;
using Game.Domain.ReadModels.CountryAggregate;
using Game.Domain.ReadModels.GameAggregate;
using Game.Domain.ReadModels.RoomAggregate;
using Game.Domain.ReadModels.UserAggregate;
using Microsoft.EntityFrameworkCore;

namespace Game.Infrastructure.Contexts
{
    public sealed class GameReadDbContext : DbContext
    {
        public DbSet<CountryReadModel> Country {  get; set; }
        public DbSet<GameReadModel> Games { get; set; }
        public DbSet<RoomReadModel> Rooms { get; set; }
        public DbSet<GameUserReadModel> Users { get; set; }
        public DbSet<RoomMemberReadModel> RoomMembers { get; set; }

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
        }
    }
}
