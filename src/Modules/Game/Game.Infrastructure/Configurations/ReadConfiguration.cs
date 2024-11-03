using Game.Domain.DomainModels.ReadModels.GameAggregate;
using Game.Domain.DomainModels.ReadModels.RoomAggregate;
using Game.Domain.DomainModels.ReadModels.UserAggregate;
using Game.Infrastructure.Seed;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Game.Infrastructure.Configurations
{
    public class ReadConfiguration : IEntityTypeConfiguration<CityReadModel>, IEntityTypeConfiguration<CountryReadModel>,
        IEntityTypeConfiguration<GameReadModel>, IEntityTypeConfiguration<OrganizerReadModel>, IEntityTypeConfiguration<PlayerReadModel>,
        IEntityTypeConfiguration<RoomMemberReadModel>, IEntityTypeConfiguration<RoomReadModel>, IEntityTypeConfiguration<GameUserReadModel>,
        IEntityTypeConfiguration<CountryPatternReadModel>, IEntityTypeConfiguration<CityPatternReadModel>
    {
        public void Configure(EntityTypeBuilder<CountryPatternReadModel> builder)
        {
            builder.HasKey(countryPattern => countryPattern.Id);

            builder
                .HasMany(countryPattern => countryPattern.CityPatterns)
                .WithOne(cityPattern => cityPattern.CountryPattern)
                .HasForeignKey(cityPattern => cityPattern.CountryId);

            builder.ToTable("CountryPatterns");
        }

        public void Configure(EntityTypeBuilder<CityPatternReadModel> builder)
        {
            builder.HasKey(cityPattern => cityPattern.Id);

            builder.ToTable("CityPatterns");
        }
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
            builder.HasKey(game=>game.RoomId);

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
            builder.HasKey(member=>new { member.GameUserId, member.RoomId});

            builder
                .HasDiscriminator<string>("RoomMemberRole")
                .HasValue<OrganizerReadModel>("Organizer")
                .HasValue<PlayerReadModel>("Player");

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

            builder
                .HasMany(room => room.Countries)
                .WithOne(countries => countries.Room)
                .HasForeignKey(countries => countries.RoomId);

            builder.ToTable("Rooms");
        }

        public void Configure(EntityTypeBuilder<GameUserReadModel> builder)
        {
            builder.HasKey(user=>user.Id);

            builder
                .HasMany(user => user.Rooms)
                .WithOne(room => room.Creator)
                .HasForeignKey(creator => creator.CreatorId);

            builder
                .HasMany(user=>user.CreatedMembers)
                .WithOne(member=>member.GameUser)
                .HasForeignKey(member => member.GameUserId);

            builder.ToTable("GameUsers");
        }
    }
}
