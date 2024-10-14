using Microsoft.AspNetCore.Identity;

namespace Identity.Infrastructure.Models
{
    public class AuthUser : IdentityUser
    {
        public string RefreshToken { get; set; } = string.Empty;
        public DateTime RefreshTokenExpires { get; set; }
    }
}
