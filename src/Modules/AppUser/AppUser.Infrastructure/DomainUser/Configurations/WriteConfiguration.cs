using AppUser.Domain.Entities;
using AppUser.Domain.Entities.Relationships;
using AppUser.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserAccess.Domain.Entities;
using WorldDomination.Shared.Domain;

namespace AppUser.Infrastructure.DomainUser.Configurations
{
    internal class WriteConfiguration : IEntityTypeConfiguration<User>, IEntityTypeConfiguration<Achievment>,
        IEntityTypeConfiguration<UserAchievment>, IEntityTypeConfiguration<ActivityStatus>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);

            builder
                .Property(u => u.Id)
                .HasConversion(id => id.Value, id => new IdValueObject(id));

            builder
                .HasOne(u => u.ActivityStatus)
                .WithOne(a => a.User)
                .HasForeignKey<ActivityStatus>(a=>a.UserId);

            builder.ToTable("Users");
        }

        public void Configure(EntityTypeBuilder<Achievment> builder)
        {
            builder.HasKey(a => a.Id);

            builder
                .Property(a => a.Id)
                .HasConversion(id => id.Value, id => new IdValueObject(id));

            builder.ToTable("Achievments");
        }

        public void Configure(EntityTypeBuilder<UserAchievment> builder)
        {
            builder.HasKey(ua => new { ua.UserId, ua.AchievmentId });

            builder
                .HasOne(u => u.User)
                .WithMany(u => u.UserAchievments)
                .HasForeignKey(ua => ua.UserId);

            builder
                .HasOne(ua => ua.Achievment)
                .WithMany(a => a.UserAchievments)
                .HasForeignKey(ua => ua.AchievmentId);

            builder
                .Property(ua => ua.AchievedTime)
                .HasDefaultValueSql("GETDATE()");

            builder.ToTable("UserAchievments");
        }

        public void Configure(EntityTypeBuilder<ActivityStatus> builder)
        {
            builder
                .HasKey(a => a.UserId);

            builder
                .Property(status => status.UserId)
                .HasConversion(id => id.Value, id => new IdValueObject(id));

            builder
                .Property(status => status.IsInGameStatus)
                .HasConversion(isingamestatus => isingamestatus.Value, isingamestatus =>new IsInGameStatus(isingamestatus));

            builder.ToTable("ActivityStatuses");
        }
    }
}
