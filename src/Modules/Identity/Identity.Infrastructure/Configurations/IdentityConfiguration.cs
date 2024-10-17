using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Identity.Infrastructure.Models;

namespace Identity.Infrastructure.Configurations
{
    internal class IdentityConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> modelBuilder)
        {
            modelBuilder.HasData(Seed.Roles);
        }
    }
}
