using AppUser.Infrastructure.Identity.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace AppUser.Infrastructure.Identity.Contexts
{
    public sealed class AuthDbContext : IdentityDbContext<AuthUser>
    {
    }
}
