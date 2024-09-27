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
                .Property(u => u.Username)
                .HasConversion(name => name.Value, name => new Username(name));

            builder
                .Property(u => u.Email)
                .HasConversion(name => name.Value, name => new Email(name));

            builder.HasOne(u => u.ActivityStatus);

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
            builder.HasKey(status => status.UserId);

            builder.HasOne(status => status.User);

            builder.ToTable("ActivityStatuses");
        }
    }
}
