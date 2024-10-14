using User.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace User.WebUI
{
    public static class Extensions
    {
        public static IServiceCollection AddUserModule(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddUserInfrastructure(configuration);

            return services;
        }

        public static IApplicationBuilder UseUserModule(this IApplicationBuilder app)
        {
            return app;
        }
    }
}
