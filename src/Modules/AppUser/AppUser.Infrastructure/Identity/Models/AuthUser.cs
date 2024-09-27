using Microsoft.AspNetCore.Identity;

namespace AppUser.Infrastructure.Identity.Models
{
    public class AuthUser : IdentityUser
    {
        public string RefresfToken { get; set; } = string.Empty;
        public DateTime RefreshTokenExpires { get; set; }
    }
}
