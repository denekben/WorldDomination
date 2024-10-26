﻿using Game.Infrastructure.ReadModels.CountryAggregate;
using Game.Infrastructure.ReadModels.GameAggregate;
using Game.Infrastructure.ReadModels.RoomAggregate;
using Game.Infrastructure.ReadModels.UserAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Game.Infrastructure.Configurations
{
    public class ReadConfiguration : IEntityTypeConfiguration<CityReadModel>, IEntityTypeConfiguration<CountryReadModel>,
        IEntityTypeConfiguration<GameReadModel>, IEntityTypeConfiguration<OrganizerReadModel>, IEntityTypeConfiguration<PlayerReadModel>,
        IEntityTypeConfiguration<RoomMemberReadModel>, IEntityTypeConfiguration<RoomReadModel>, IEntityTypeConfiguration<GameUserReadModel>
    {
        public void Configure(EntityTypeBuilder<CityReadModel> builder)
        {
            builder.HasKey(city=>city.Id);

            builder.ToTable("Cities");
        }

        public void Configure(EntityTypeBuilder<CountryReadModel> builder)
        {
            builder.HasKey(country=>country.Id);

            builder
                .HasMany(country => country.Players)
                .WithOne(player => player.Country)
                .HasForeignKey(player => player.CountryId);

            builder
                .HasMany(country => country.Cities)
                .WithOne(city => city.Country)
                .HasForeignKey(city => city.CountryId);

            builder.ToTable("Countries");
        }

        public void Configure(EntityTypeBuilder<GameReadModel> builder)
        {
            builder.HasKey(game=>game.Id);

            builder
                .HasMany(game => game.Countries)
                .WithOne(country => country.Game)
                .HasForeignKey(country => country.GameId);

            builder.ToTable("Games");
        }
        public void Configure(EntityTypeBuilder<OrganizerReadModel> builder)
        {
            throw new NotImplementedException();
        }

        public void Configure(EntityTypeBuilder<PlayerReadModel> builder)
        {
            throw new NotImplementedException();
        }

        public void Configure(EntityTypeBuilder<RoomMemberReadModel> builder)
        {
            builder.HasKey(member=>member.Id);

            builder.ToTable("RoomMembers");
        }

        public void Configure(EntityTypeBuilder<RoomReadModel> builder)
        {
            builder.HasKey(room=>room.Id);

            builder
                .HasOne(room => room.Game)
                .WithOne(game => game.Room)
                .HasForeignKey<GameReadModel>(game => game.RoomId);

            builder
                .HasMany(room => room.RoomMembers)
                .WithOne(members => members.Room)
                .HasForeignKey(members => members.RoomId);

            builder.ToTable("Rooms");
        }

        public void Configure(EntityTypeBuilder<GameUserReadModel> builder)
        {
            builder.HasKey(user=>user.Id);

            builder
                .HasMany(user => user.Rooms)
                .WithOne(room => room.Creator)
                .HasForeignKey(creator => creator.CreatorId);

            builder.ToTable("GameUsers");
        }
    }
}