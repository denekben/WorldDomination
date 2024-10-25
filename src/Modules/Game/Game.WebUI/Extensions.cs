using Game.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace Game.WebUI
{
    public static class Extensions
    {
        public static IServiceCollection AddGameModule(this IServiceCollection services)
        {
            services.AddGameInfrastructure();

            return services;
        }
    }
}
