using User.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using User.Infrastructure.ReadModels;

namespace User.Infrastructure.Configurations
{
    internal sealed class ReadConfiguration : IEntityTypeConfiguration<UserReadModel>,
        IEntityTypeConfiguration<AchievmentReadModel>, IEntityTypeConfiguration<UserAchievmentReadModel>, IEntityTypeConfiguration<UserStatusReadModel>
    {
        public void Configure(EntityTypeBuilder<UserReadModel> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(u => u.Id);

            builder
                .HasOne(u => u.UserStatus)
                .WithOne(a => a.User)
                .HasForeignKey<UserStatusReadModel>(a => a.UserId);
        }

        public void Configure(EntityTypeBuilder<AchievmentReadModel> builder)
        {
            builder.ToTable("Achievments");

            builder.HasKey(ach => ach.Id);
        }

        public void Configure(EntityTypeBuilder<UserAchievmentReadModel> builder)
        {
            builder.ToTable("UserAchievments");

            builder.HasKey(ua => new { ua.UserId, ua.AchievmentId });

            builder
                .HasOne(ua => ua.User)
                .WithMany(u => u.UserAchievments)
                .HasForeignKey(ua => ua.UserId);

            builder
                .HasOne(ua => ua.Achievment)
                .WithMany(a => a.UserAchievments)
                .HasForeignKey(ua => ua.AchievmentId);
        }

        public void Configure(EntityTypeBuilder<UserStatusReadModel> builder)
        {
            builder.ToTable("UserStatuses");

            builder.HasKey(status => status.UserId);
        }
    }
}
