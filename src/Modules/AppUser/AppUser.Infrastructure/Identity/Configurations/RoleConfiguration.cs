using AppUser.Infrastructure.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppUser.Infrastructure.Identity.Configurations
{
    public class RoleConfiguration
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
