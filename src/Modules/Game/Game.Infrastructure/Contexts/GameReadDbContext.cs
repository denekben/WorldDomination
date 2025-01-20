using Game.Infrastructure.Configurations;
using Game.Domain.DomainModels.ReadModels.Games;
using Game.Domain.DomainModels.ReadModels.Rooms;
using Game.Domain.DomainModels.ReadModels.Users;
using Microsoft.EntityFrameworkCore;
using Game.Infrastructure.Seed;
using Game.Domain.ReadModels.Games;
using Game.Domain.ReadModels.Messaging;

namespace Game.Infrastructure.Contexts
{
    public sealed class GameReadDbContext : DbContext
    {
        public DbSet<RoomReadModel> Rooms { get; set; }
        public DbSet<CountryPatternReadModel> CountryPatterns { get; set; }
        public DbSet<CityPatternReadModel> CityPatterns { get; set; }
        public DbSet<CountryReadModel> Countries { get; set; }
        public DbSet<SanctionReadModel> Sanctions { get; set; }
        public DbSet<GameUserReadModel> Users { get; set; }
        public DbSet<RoomMemberReadModel> RoomMembers { get; set; }
        public DbSet<GameReadModel> Games { get; set; }
        public DbSet<NegotiationRequestReadModel> NegotiationRequests { get; set; }

        public GameReadDbContext(DbContextOptions<GameReadDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("Game");

            var configuration = new ReadConfiguration();

            modelBuilder.ApplyConfiguration<CityReadModel>(configuration);
            modelBuilder.ApplyConfiguration<CountryReadModel>(configuration);
            modelBuilder.ApplyConfiguration<SanctionReadModel>(configuration);

            modelBuilder.ApplyConfiguration<GameReadModel>(configuration);

            //modelBuilder.ApplyConfiguration<OrganizerReadModel>(configuration);
            //modelBuilder.ApplyConfiguration<PlayerReadModel>(configuration);
            modelBuilder.ApplyConfiguration<RoomMemberReadModel>(configuration);
            modelBuilder.ApplyConfiguration<RoomReadModel>(configuration);

            modelBuilder.ApplyConfiguration<GameUserReadModel>(configuration);

            modelBuilder.ApplyConfiguration<CityPatternReadModel>(configuration);
            modelBuilder.ApplyConfiguration<CountryPatternReadModel>(configuration);

            modelBuilder.ApplyConfiguration<SanctionReadModel>(configuration);

            modelBuilder.ApplyConfiguration<OrderReadModel>(configuration);

            modelBuilder.ApplyConfiguration<MessageReadModel>(configuration);
            modelBuilder.ApplyConfiguration<NegotiationChatReadModel>(configuration);
            modelBuilder.ApplyConfiguration<NegotiationRequestReadModel>(configuration);
        }
    }
}
