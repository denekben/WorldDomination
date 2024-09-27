using AppUser.Infrastructure.DomainUser;
using AppUser.Infrastructure.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AppUser.Infrastructure
{
    public static class Extensions
    {
        public static IServiceCollection AddUserInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddUserDAL();
            services.AddUserIdentity(configuration);

            return services;
        }
    }
}
