using Identity.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Identity.WebUI
{
    public static class Extensions
    {
        public static IServiceCollection AddIdentityModule(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddIdentityInfrastructure(configuration);

            return services;
        }

        public static IApplicationBuilder UseIdentityModule(this IApplicationBuilder app)
        {
            return app;
        }
    }
}
