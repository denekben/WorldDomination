using AppUser.Infrastructure.DomainUser.ReadModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppUser.Infrastructure.DomainUser.Configurations
{
    internal sealed class ReadConfiguration : IEntityTypeConfiguration<UserReadModel>, 
        IEntityTypeConfiguration<AchievmentReadModel>, IEntityTypeConfiguration<UserAchievmentReadModel> , IEntityTypeConfiguration<ActivityStatusReadModel>
    {
        public void Configure(EntityTypeBuilder<UserReadModel> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(u=>u.Id);
        }

        public void Configure(EntityTypeBuilder<AchievmentReadModel> builder)
        {
            builder.ToTable("Achievments");

            builder.HasKey(ach=>ach.Id);
        }

        public void Configure(EntityTypeBuilder<UserAchievmentReadModel> builder)
        {
            builder.ToTable("UserAchievments");

            builder.HasKey(ua => new { ua.UserId, ua.AchievmentId });

            builder
                .HasOne(ua => ua.UserReadModel)
                .WithMany(u => u.UserAchievments)
                .HasForeignKey(ua => ua.UserId);

            builder
                .HasOne(ua => ua.AchievmentReadModel)
                .WithMany(a => a.UserAchievments)
                .HasForeignKey(ua => ua.AchievmentId);
        }

        public void Configure(EntityTypeBuilder<ActivityStatusReadModel> builder)
        {
            builder.ToTable("ActivityStatuses");

            builder.HasKey(status => status.UserId);
        }
    }
}
