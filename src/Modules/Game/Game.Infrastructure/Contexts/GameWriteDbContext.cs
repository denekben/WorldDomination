using Game.Domain.CountryAggregate.Entities;
using Game.Domain.RoomAggregate.Entities;
using Game.Domain.UserAggregate.Entities;
using DomainGame = Game.Domain.GameAggregate.Entities.Game;
using Game.Infrastructure.Configurations;
using Game.Infrastructure.Seed;
using Microsoft.EntityFrameworkCore;
using WorldDomination.Shared.Domain;
using Game.Domain.DomainModels.RoomAggregate.Abstractions;

namespace Game.Infrastructure.Contexts
{
    public sealed class GameWriteDbContext : DbContext
    {
        public DbSet<GameUser> Users { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<RoomMember> Members { get; set; }
        public DbSet<DomainGame> Games { get; set; }

        public GameWriteDbContext(DbContextOptions<GameWriteDbContext> options) 
            : base(options) 
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("Game");

            var configuration = new WriteConfiguration();

            modelBuilder.ApplyConfiguration<City>(configuration);
            modelBuilder.ApplyConfiguration<Country>(configuration);

            modelBuilder.ApplyConfiguration<DomainGame>(configuration);

            //modelBuilder.ApplyConfiguration<Organizer>(configuration);
            //modelBuilder.ApplyConfiguration<Player>(configuration);
            modelBuilder.ApplyConfiguration<RoomMember>(configuration);
            modelBuilder.ApplyConfiguration<Room>(configuration);

            modelBuilder.ApplyConfiguration<GameUser>(configuration);

            modelBuilder.ApplyConfiguration<CountryPattern>(configuration);
            modelBuilder.ApplyConfiguration<CityPattern>(configuration);

            modelBuilder.Ignore<DomainEntity>();

            base.OnModelCreating(modelBuilder);
        }
    }
}
