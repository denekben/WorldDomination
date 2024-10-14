using User.Domain.Entities;
using User.Domain.Entities.Relationships;
using User.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorldDomination.Shared.Domain;

namespace User.Infrastructure.Configurations
{
    internal class WriteConfiguration : IEntityTypeConfiguration<DomainUser>, IEntityTypeConfiguration<Achievment>,
        IEntityTypeConfiguration<UserAchievment>, IEntityTypeConfiguration<UserStatus>
    {
        public void Configure(EntityTypeBuilder<DomainUser> builder)
        {
            builder.HasKey(u => u.Id);

            builder
                .Property(u => u.Id)
                .HasConversion(id => id.Value, id => new IdValueObject(id));

            builder
                .HasOne(u => u.UserStatus)
                .WithOne(a => a.User)
                .HasForeignKey<UserStatus>(a => a.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .Property(u=>u.Name)
                .HasConversion(name => name.Value, name=> Name.Create(name));

            builder
                .Property(u => u.Bio)
                .HasConversion(bio => bio.Value, bio => Bio.Create(bio));

            builder
                .Property(u => u.ProfileImagePath)
                .HasConversion(path => path.Value, path => ProfileImagePath.Create(path));


            builder
                .Property(u => u.CreatedTime)
                .HasDefaultValue(DateTime.UtcNow)
                .ValueGeneratedOnAdd();

            builder
                .Property(u => u.UpdatedTime)
                .HasDefaultValue(DateTime.UtcNow)
                .ValueGeneratedOnUpdate();

            builder.ToTable("Users");
        }

        public void Configure(EntityTypeBuilder<Achievment> builder)
        {
            builder.HasKey(a => a.Id);

            builder
                .Property(a => a.Id)
                .HasConversion(id => id.Value, id => new IdValueObject(id));

            builder
                .Property(a => a.CreatedTime)
                .HasDefaultValue(DateTime.UtcNow)
                .ValueGeneratedOnAdd();

            builder
                .Property(a => a.UpdatedTime)
                .HasDefaultValue(DateTime.UtcNow)
                .ValueGeneratedOnUpdate();

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
                .HasDefaultValue(DateTime.UtcNow)
                .ValueGeneratedOnAdd();

            builder.ToTable("UserAchievments");
        }

        public void Configure(EntityTypeBuilder<UserStatus> builder)
        {
            builder
                .HasKey(a => a.UserId);

            builder
                .Property(status => status.UserId)
                .HasConversion(id => id.Value, id => new IdValueObject(id));

            builder
                .Property(status => status.ActivityStatus)
                .HasConversion(activityStatus => activityStatus.Value, activityStatus => ActivityStatus.Create(activityStatus));

            builder.ToTable("UserStatuses");
        }
    }
}
