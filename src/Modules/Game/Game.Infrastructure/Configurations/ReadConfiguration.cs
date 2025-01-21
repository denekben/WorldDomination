using Game.Domain.DomainModels.Games.Entities;
using Game.Domain.DomainModels.ReadModels.Games;
using Game.Domain.DomainModels.ReadModels.Rooms;
using Game.Domain.DomainModels.ReadModels.Users;
using Game.Domain.ReadModels.Games;
using Game.Domain.ReadModels.Messaging;
using Game.Infrastructure.Seed;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorldDomination.Shared.Domain;

namespace Game.Infrastructure.Configurations
{
    public class ReadConfiguration : IEntityTypeConfiguration<CityReadModel>, IEntityTypeConfiguration<CountryReadModel>,
        IEntityTypeConfiguration<GameReadModel>, IEntityTypeConfiguration<OrganizerReadModel>, IEntityTypeConfiguration<PlayerReadModel>,
        IEntityTypeConfiguration<RoomMemberReadModel>, IEntityTypeConfiguration<RoomReadModel>, IEntityTypeConfiguration<GameUserReadModel>,
        IEntityTypeConfiguration<CountryPatternReadModel>, IEntityTypeConfiguration<CityPatternReadModel>, IEntityTypeConfiguration<SanctionReadModel>,
        IEntityTypeConfiguration<OrderReadModel>, IEntityTypeConfiguration<MessageReadModel>, IEntityTypeConfiguration<NegotiationChatReadModel>,
        IEntityTypeConfiguration<NegotiationRequestReadModel>, IEntityTypeConfiguration<GameEventReadModel>
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

            builder
                .HasMany(country=>country.OutgoingSanctions)
                .WithOne(sanction=>sanction.Issuer)
                .HasForeignKey(sanction=>sanction.IssuerId);

            builder
                .HasMany(country => country.IncomingSanctions)
                .WithOne(sanction => sanction.Audience)
                .HasForeignKey(sanction => sanction.AudienceId);

            builder
                .HasOne(country => country.Order)
                .WithOne(order => order.Country)
                .HasForeignKey<OrderReadModel>(order => order.CountryId);

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

        public void Configure(EntityTypeBuilder<SanctionReadModel> builder)
        {
            builder.HasKey(s => new { s.IssuerId, s.AudienceId });

            builder
                .HasOne(s => s.Issuer)
                .WithMany(c => c.OutgoingSanctions)
                .HasForeignKey(s => s.IssuerId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasOne(s => s.Audience)
                .WithMany(c => c.IncomingSanctions)
                .HasForeignKey(s => s.AudienceId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.ToTable("Sanctions");
        }

        public void Configure(EntityTypeBuilder<OrderReadModel> builder)
        {
            builder.HasKey(o => o.CountryId);

            builder
                .Property(o => o.CountriesToDonate)
                .HasConversion(ctd => ctd.SerializeDictionaryForGuid(), ctd => ctd.DeserializeDictionaryForGuid());

            builder.ToTable("Orders");
        }

        public void Configure(EntityTypeBuilder<MessageReadModel> builder)
        {
            builder.HasKey(m => m.Id);

            //builder
            //    .HasOne(m => m.Issuer)
            //    .WithMany(i => i.Messages)
            //    .HasForeignKey(m => m.IssuerId);

            builder.ToTable("Messages");
        }

        public void Configure(EntityTypeBuilder<NegotiationChatReadModel> builder)
        {
            builder.HasKey(nc => nc.Id);

            builder.ToTable("NegotiationChats");
        }

        public void Configure(EntityTypeBuilder<NegotiationRequestReadModel> builder)
        {
            builder.HasKey(nr => new { nr.IssuerCountryId, nr.AudienceCountryId, nr.IssuerMemberId });

            builder
                .HasOne(nr => nr.Issuer)
                .WithMany(c => c.OutgoingRequests)
                .HasForeignKey(c => c.IssuerCountryId);

            builder
                .HasOne(nr => nr.Audience)
                .WithMany(c => c.IncomingRequests)
                .HasForeignKey(c => c.AudienceCountryId);

            builder.ToTable("NegotiationRequests");
        }

        public void Configure(EntityTypeBuilder<GameEventReadModel> builder)
        {
            builder.HasKey(ge => ge.Id);

            builder.ToTable("GameEvents");
        }
    }
}