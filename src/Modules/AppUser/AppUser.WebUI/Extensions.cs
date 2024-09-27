using AppUser.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AppUser.WebUI
{
    public static class Extensions
    {
        public static IServiceCollection AddAppUserModule(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAppUserInfrastructure(configuration);

            return services;
        }

        public static IApplicationBuilder UseAppUserModule(this IApplicationBuilder app)
        {
            return app;
        }
    }
}
