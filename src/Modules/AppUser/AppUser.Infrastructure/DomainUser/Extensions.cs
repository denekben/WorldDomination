using AppUser.Application.Services;
using AppUser.Infrastructure.DomainUser.Contexts;
using AppUser.Infrastructure.Identity.Services;
using Microsoft.Extensions.DependencyInjection;
using Shared.Postgres;

namespace AppUser.Infrastructure.DomainUser
{
    public static class Extensions
    {
        public static IServiceCollection AddUserDAL(this IServiceCollection services)
        {
            services.AddPostgres<UserReadDbContext>();
            services.AddPostgres<UserWriteDbContext>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IAuthService, AuthService>();

            return services;
        }
    }
}
