using Microsoft.Extensions.DependencyInjection;
using User.Infrastructure;

namespace User.WebUI
{
    public static class Extensions
    {
        public static IServiceCollection AddUserModule(this IServiceCollection services)
        {
            services.AddUserInfrastructure();
            
            return services;
        }
    }
}
