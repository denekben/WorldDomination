using Game.Domain.DomainModels.Games.Entities;
using Game.Domain.DomainModels.Rooms.Entities;
using Game.Infrastructure.Seed;
using DomainGame = Game.Domain.DomainModels.Games.Entities.Game;
using WorldDomination.Shared.Domain;
using Game.Domain.DomainModels.Users.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Game.Domain.DomainModels.Rooms.ValueObjects;
using Game.Domain.DomainModels.Games.ValueObjects;

namespace Game.Infrastructure.Configurations
{
    internal class WriteConfiguration : IEntityTypeConfiguration<CountryPattern>, IEntityTypeConfiguration<CityPattern>, IEntityTypeConfiguration<City>,
        IEntityTypeConfiguration<Country>, IEntityTypeConfiguration<DomainGame>, IEntityTypeConfiguration<Organizer>,
        IEntityTypeConfiguration<Player>, IEntityTypeConfiguration<Room>, IEntityTypeConfiguration<GameUser>, IEntityTypeConfiguration<RoomMember>,
        IEntityTypeConfiguration<Sanction>
    {
        public void Configure(EntityTypeBuilder<CountryPattern> builder)
        {
            builder.HasKey(countryPattern => countryPattern.Id);

            builder
                .Property(countryPattern => countryPattern.Id)
                .HasConversion(id => id.Value, id => new IdValueObject(id));

            builder
                .HasMany(countryPattern => countryPattern.CityPatterns)
                .WithOne(cityPattern => cityPattern.CountryPattern)
                .HasForeignKey(cityPattern => cityPattern.CountryId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasData(Seed.Seed.Countries);

            builder.ToTable("CountryPatterns");
        }

        public void Configure(EntityTypeBuilder<CityPattern> builder)
        {
            builder.HasKey(cityPattern => cityPattern.Id);

            builder
                .Property(cityPattern => cityPattern.Id)
                .HasConversion(id => id.Value, id => new IdValueObject(id));

            builder.HasData(Seed.Seed.Cities);

            builder.ToTable("CityPatterns");
        }

        public void Configure(EntityTypeBuilder<City> builder)
        {
            builder.HasKey(city => city.Id);

            builder
                .Property(city => city.Id)
                .HasConversion(id => id.Value, id => new IdValueObject(id));

            builder
                .Property(city => city.DevelopmentLevel)
                .HasConversion(dLevel => dLevel.Value, dLevel => DevelopmentLevel.Create(dLevel));

            builder
                .Property(city => city.LivingLevel)
                .HasConversion(lLevel => lLevel.Value, lLevel => LivingLevel.Create(lLevel));

            builder
                .Property(city => city.Income)
                .HasConversion(income => income.Value, income => Income.Create(income));

            builder.ToTable("Cities");
        }

        public void Configure(EntityTypeBuilder<Country> builder)
        {
            builder.HasKey(country => country.Id);

            builder
                .Property(country => country.Id)
                .HasConversion(id => id.Value, id => new IdValueObject(id));

            builder
                .HasMany(country => country.Players)
                .WithOne(player => player.Country)
                .HasForeignKey(player => player.CountryId)
                .OnDelete(DeleteBehavior.SetNull);

            builder
                .HasMany(country => country.Cities)
                .WithOne(city => city.Country)
                .HasForeignKey(city => city.CountryId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasMany(country => country.Sanctions)
                .WithOne(sanction => sanction.Issuser)
                .HasForeignKey(sanction => sanction.IssuserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .Property(country => country.LivingLevel)
                .HasConversion(lLevel => lLevel.Value, lLevel => LivingLevel.Create(lLevel));

            builder
                .Property(country => country.Budget)
                .HasConversion(budget => budget.Value, budget => Budget.Create(budget));

            builder
                .Property(country => country.NuclearTechnology)
                .HasConversion(nTechnology => nTechnology.Value, nTechnology => NuclearTechnology.Create(nTechnology));

            builder.ToTable("Countries");
        }

        public void Configure(EntityTypeBuilder<DomainGame> builder)
        {
            builder.HasKey(game => game.RoomId);

            builder
                .Property(game => game.RoomId)
                .HasConversion(id => id.Value, id => new IdValueObject(id));

            builder
                .HasMany(game => game.Countries)
                .WithOne(country => country.Game)
                .HasForeignKey(country => country.GameId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .Property(game => game.GameType)
                .HasConversion(gameType => gameType.Value, gameType => GameType.Create(gameType));

            builder
                .Property(game => game.CurrentRound)
                .HasConversion(currentRound => currentRound.Value, currentRound => CurrentRound.Create(currentRound));

            builder
                .Property(game => game.EcologyLevel)
                .HasConversion(eLevel => eLevel.Value, eLevel => EcologyLevel.Create(eLevel));

            builder.ToTable("Games");
        }

        public void Configure(EntityTypeBuilder<Organizer> builder)
        {
            throw new NotImplementedException();
        }

        public void Configure(EntityTypeBuilder<Player> builder)
        {
            throw new NotImplementedException();
        }

        public void Configure(EntityTypeBuilder<Room> builder)
        {
            builder.HasKey(room => room.Id);

            builder
                .Property(room => room.Id)
                .HasConversion(id => id.Value, id => new IdValueObject(id));

            builder
                .HasOne(room => room.DomainGame)
                .WithOne(game => game.Room)
                .HasForeignKey<DomainGame>(game => game.RoomId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasMany(room => room.RoomMembers)
                .WithOne(members => members.Room)
                .HasForeignKey(members => members.RoomId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasMany(room => room.Countries)
                .WithOne(countries => countries.Room)
                .HasForeignKey(countries => countries.RoomId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .Property(room => room.RoomName)
                .HasConversion(name => name.Value, name => RoomName.Create(name));

            builder
                .Property(room => room.GameType)
                .HasConversion(gameType => gameType.Value, gameType => GameType.Create(gameType));

            builder
                .Property(room => room.RoomMemberLimit)
                .HasConversion(memberLimit => memberLimit.Value, memberLimit => RoomMemberLimit.Create(memberLimit));

            builder
                .Property(room => room.CountryLimit)
                .HasConversion(countryLimit => countryLimit.Value, countryLimit => CountryLimit.Create(countryLimit));

            builder
                .Property(room => room.CreatedTime)
                .HasDefaultValue(DateTime.UtcNow)
                .ValueGeneratedOnAdd();

            builder.ToTable("Rooms");
        }

        public void Configure(EntityTypeBuilder<GameUser> builder)
        {
            builder.HasKey(user => user.Id);

            builder
                .Property(user => user.Id)
                .HasConversion(id => id.Value, id => new IdValueObject(id));

            builder
                .HasMany(user => user.Rooms)
                .WithOne(room => room.Creator)
                .HasForeignKey(room => room.CreatorId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasMany(user => user.CreatedMembers)
                .WithOne(member => member.GameUser)
                .HasForeignKey(member => member.GameUserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.ToTable("GameUsers");
        }

        public void Configure(EntityTypeBuilder<RoomMember> builder)
        {
            builder.HasKey(member => new { member.GameUserId, member.RoomId });

            builder
                .Property(member => member.GameUserId)
                .HasConversion(id => id.Value, id => new IdValueObject(id));

            builder
                .Property(member => member.GameRole)
                .HasConversion(role => role.Value, role => GameRole.Create(role));

            builder
                .HasDiscriminator<string>("RoomMemberRole")
                .HasValue<Organizer>("Organizer")
                .HasValue<Player>("Player");

            builder
                .ToTable("RoomMembers");
        }

        public void Configure(EntityTypeBuilder<Sanction> builder)
        {
            builder.HasKey(s => new { s.IssuserId, s.AudienceId });

            builder
                .Property(s => s.SanctionPower)
                .HasConversion(sp => sp.Value, sp => SanctionPower.Create(sp));

            builder.ToTable("Sanctions");
        }

    }
}
