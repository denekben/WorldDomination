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
            List<IdentityRole> roles = new List<IdentityRole> {
                new IdentityRole {
                    Name = UserRole.Admin,
                    NormalizedName = UserRole.Admin.ToUpper()
                },
                new IdentityRole {
                    Name = UserRole.Member,
                    NormalizedName = UserRole.Member.ToUpper()
                }
            };
            modelBuilder.HasData(roles);
        }
    }
}
