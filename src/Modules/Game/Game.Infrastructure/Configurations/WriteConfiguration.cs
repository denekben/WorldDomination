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
using Game.Domain.DomainModels.Messaging.Entities;
using Game.Domain.DomainModels.Messaging.ValueObjects;

namespace Game.Infrastructure.Configurations
{
    internal class WriteConfiguration : IEntityTypeConfiguration<CountryPattern>, IEntityTypeConfiguration<CityPattern>, IEntityTypeConfiguration<City>,
        IEntityTypeConfiguration<Country>, IEntityTypeConfiguration<DomainGame>, IEntityTypeConfiguration<Organizer>,
        IEntityTypeConfiguration<Player>, IEntityTypeConfiguration<Room>, IEntityTypeConfiguration<GameUser>, IEntityTypeConfiguration<RoomMember>,
        IEntityTypeConfiguration<Sanction>, IEntityTypeConfiguration<Order>, IEntityTypeConfiguration<Message>, IEntityTypeConfiguration<NegotiationChat>,
        IEntityTypeConfiguration<NegotiationRequest>, IEntityTypeConfiguration<GameEvent>
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
                .HasMany(country => country.OutgoingSanctions)
                .WithOne(sanction => sanction.Issuer)
                .HasForeignKey(sanction => sanction.IssuerId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasMany(country => country.IncomingSanctions)
                .WithOne(sanction => sanction.Audience)
                .HasForeignKey(sanction => sanction.AudienceId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasOne(country=>country.Order)
                .WithOne(order=>order.Country)
                .HasForeignKey<Order>(order => order.CountryId) 
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

            builder
                .Property(country=>country.Income)
                .HasConversion(income=>income.Value, income => Income.Create(income));

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
                .Property(game=>game.RoundQuantity)
                .HasConversion(roundQuantity=>roundQuantity.Value, roundQuantity=>RoundQuantity.Create(roundQuantity));

            builder
                .Property(game => game.EcologyLevel)
                .HasConversion(eLevel => eLevel.Value, eLevel => EcologyLevel.Create(eLevel));

            builder
                .Property(game => game.GameState)
                .HasConversion(state => state.Value, state => GameState.Create(state));

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
                .Property(room => room.RoundQuantity)
                .HasConversion(roundQuantity => roundQuantity.Value, roundQuantity => RoundQuantity.Create(roundQuantity));

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
            builder.HasKey(s => new { s.IssuerId, s.AudienceId });

            builder
                .Property(s => s.AudienceId)
                .HasConversion(id=>id.Value, id=>new IdValueObject(id));

            builder
                .Property(s => s.IssuerId)
                .HasConversion(id => id.Value, id => new IdValueObject(id));

            builder
                .Property(s => s.SanctionPower)
                .HasConversion(sp => sp.Value, sp => SanctionPower.Create(sp));

            builder
                .HasOne(s => s.Issuer)
                .WithMany(c => c.OutgoingSanctions)
                .HasForeignKey(s => s.IssuerId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasOne(s => s.Audience)
                .WithMany(c => c.IncomingSanctions)
                .HasForeignKey(s=>s.AudienceId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.ToTable("Sanctions");
        }

        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(o=>o.CountryId);

            builder
                .Property(o => o.CountryId)
                .HasConversion(id=>id.Value,id=>new IdValueObject(id));

            builder
                .Property(o => o.CitiesToDevelop)
                .HasConversion(ctd => ctd.SerializeList(), ctd => ctd.DeserializeList());

            builder
                .Property(o => o.CitiesToSetShield)
                .HasConversion(ctss=> ctss.SerializeList(), ctss => ctss.DeserializeList());

            builder
                .Property(o => o.CitiesToStrike)
                .HasConversion(cts=> cts.SerializeList(), cts => cts.DeserializeList());

            builder
                .Property(o=>o.CountriesToSetSanctions)
                .HasConversion(ctss => ctss.SerializeList(), ctss => ctss.DeserializeList());

            builder
                .Property(o => o.CountriesToDonate)
                .HasConversion(ctd => ctd.SerializeDictionary(), ctd => ctd.DeserializeDictionary());

            builder
                .Property(o => o.RoomId)
                .HasConversion(id => id.Value, id => new IdValueObject(id));

            builder.ToTable("Orders");
        }

        public void Configure(EntityTypeBuilder<Message> builder)
        {
            builder.HasKey(m=>m.Id);

            builder
                .Property(m => m.Id)
                .HasConversion(id=>id.Value, id=>new IdValueObject(id));

            builder
                .Property(m => m.MessageText)
                .HasConversion(mt=>mt.Value, mt=>MessageText.Create(mt));

            builder
                .Property(m=>m.IssuerId)
                .HasConversion(iid=>iid.Value, iid=>new IdValueObject(iid));

            builder
                .Property(m => m.ChatId)
                .HasConversion(chatId=>chatId.Value, chatId => new IdValueObject(chatId));

            //builder
            //    .HasOne(m=>m.Issuer)
            //    .WithMany(i=>i.Messages)
            //    .HasForeignKey(m=>m.IssuerId)
            //    .OnDelete(DeleteBehavior.Cascade);

            builder.ToTable("Messages");
        }

        public void Configure(EntityTypeBuilder<NegotiationChat> builder)
        {
            builder.HasKey(nc => nc.Id);
            
            builder
                .Property(nc => nc.Id)
                .HasConversion(id=>id.Value, id => new IdValueObject(id));

            builder
                .Property(nc => nc.FirstCountryId)
                .HasConversion(fcId=>fcId.Value, fcId => new IdValueObject(fcId));

            builder
                .Property(nc => nc.SecondCountryId)
                .HasConversion(scId => scId.Value, scId => new IdValueObject(scId));

            builder.ToTable("NegotiationChats");
        }

        public void Configure(EntityTypeBuilder<NegotiationRequest> builder)
        {
            builder.HasKey(nr=>new { nr.IssuerCountryId, nr.AudienceCountryId, nr.IssuerMemberId });

            builder
                .Property(nr => nr.IssuerCountryId)
                .HasConversion(id => id.Value, id => new IdValueObject(id));

            builder
                .Property(nr => nr.AudienceCountryId)
                .HasConversion(id => id.Value, id => new IdValueObject(id));

            builder
                .Property(nr => nr.IssuerMemberId)
                .HasConversion(id => id.Value, id => new IdValueObject(id));

            builder
                .HasOne(nr=>nr.Issuer)
                .WithMany(c=>c.OutgoingRequests)
                .HasForeignKey(c=>c.IssuerCountryId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasOne(nr => nr.Audience)
                .WithMany(c => c.IncomingRequests)
                .HasForeignKey(c => c.AudienceCountryId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.ToTable("NegotiationRequests");
        }

        public void Configure(EntityTypeBuilder<GameEvent> builder)
        {
            builder.HasKey(ge=>ge.Id);

            builder
                .Property(ge => ge.Id)
                .HasConversion(id=>id.Value, id=>new IdValueObject(id));

            builder
                .Property(ge => ge.Quality)
                .HasConversion(q=>q.Value, q=>GameEventQuality.Create(q));

            builder.ToTable("GameEvents");

            builder.HasData(Seed.Seed.Events);
        }
    }
}
