using Identity.Infrastructure.Models;
using Microsoft.AspNetCore.Identity;

namespace Identity.Infrastructure.Configurations
{
    public static class Seed
    {
        public static List<IdentityRole> Roles { get; private set; } = [];

        static Seed() {
            Roles = [
                new IdentityRole {
                    Name = UserRole.Admin,
                    NormalizedName = UserRole.Admin.ToUpper()
                },
                new IdentityRole {
                    Name = UserRole.User,
                    NormalizedName = UserRole.User.ToUpper()
                }
            ];
        }
    }
}
