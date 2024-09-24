using AppUser.Infrastructure.EF.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace AppUser.Infrastructure.EF.Contexts
{
    internal sealed class AuthDbContext : IdentityDbContext<AuthUser>
    {
    }
}
