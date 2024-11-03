using Game.Infrastructure.Contexts;
using Game.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Shared.Postgres;
using Microsoft.EntityFrameworkCore;
using Game.Infrastructure.Realtime;
using Game.Application.Services;
using Game.Infrastructure.Services;
using Game.Domain.Repositories;

namespace Game.Infrastructure
{
    public static class Extensions
    {
        public static IServiceCollection AddGameInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<IRoomRepository, RoomRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRoomMemberRepository, RoomMemberRepository>();
            services.AddScoped<IGameRepository, GameRepository>();
            services.AddScoped<ICountryRepository, CountryRepository>();    

            services.AddPostgres<GameWriteDbContext>();
            services.AddPostgres<GameReadDbContext>(QueryTrackingBehavior.NoTracking);

            services.AddSignalR();
            services.AddScoped<IGameModuleNotificationService, GameModuleNotificationService>();
            services.AddScoped<IGameModuleService, GameModuleService>();
            services.AddScoped<ICountryFabric, CountryFabric>();

            return services;
        }
    }
}
