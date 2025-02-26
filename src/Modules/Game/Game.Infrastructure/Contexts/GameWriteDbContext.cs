﻿using Game.Domain.DomainModels.Games.Entities;
using Game.Domain.DomainModels.Rooms.Entities;
using Game.Domain.DomainModels.Users.Entities;
using DomainGame = Game.Domain.DomainModels.Games.Entities.Game;
using Game.Infrastructure.Configurations;
using Game.Infrastructure.Seed;
using Microsoft.EntityFrameworkCore;
using WorldDomination.Shared.Domain;
using Game.Domain.DomainModels.Messaging.Entities;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Game.Infrastructure.Contexts
{
    public sealed class GameWriteDbContext : DbContext
    {
        public DbSet<GameUser> Users { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<RoomMember> Members { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<DomainGame> Games { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<NegotiationChat> NegotiationChats { get; set; }
        public DbSet<NegotiationRequest> NegotiationRequests { get; set; }
        public DbSet<GameEvent> Events { get; set; }

        public GameWriteDbContext(DbContextOptions<GameWriteDbContext> options) 
            : base(options) { }

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

            modelBuilder.ApplyConfiguration<Sanction>(configuration);

            modelBuilder.ApplyConfiguration<Order>(configuration);

            modelBuilder.ApplyConfiguration<Message>(configuration);
            modelBuilder.ApplyConfiguration<NegotiationChat>(configuration);
            modelBuilder.ApplyConfiguration<NegotiationRequest>(configuration);

            modelBuilder.ApplyConfiguration<GameEvent>(configuration);

            modelBuilder.Ignore<DomainEntity>();

            base.OnModelCreating(modelBuilder);
        }
    }
}