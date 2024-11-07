using Game.Infrastructure.Contexts;
using Game.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Shared.Postgres;
using Microsoft.EntityFrameworkCore;
using Game.Infrastructure.Realtime;
using Game.Application.Services;
using Game.Domain.Interfaces.Repositories;
using Game.Domain.Interfaces.Countries;
using Game.Application.Helpers;
using Game.Infrastructure.Services;
using Game.Domain.DomainModels.Games.Strategies;

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
            services.AddScoped<IGameModuleHelper, GameModuleHelper>();
            services.AddScoped<IGameModuleReadService, GameModuleReadService>();

            services.AddScoped<ICountryFactory, CountryFactory>();

            services.AddScoped<ICountryStrategy, ChinaStrategy>();
            services.AddScoped<ICountryStrategy, CubaStrategy>();
            services.AddScoped<ICountryStrategy, FranceStrategy>();
            services.AddScoped<ICountryStrategy, GermanyStrategy>();
            services.AddScoped<ICountryStrategy, GreatBritainStrategy>();
            services.AddScoped<ICountryStrategy, IranStrategy>();
            services.AddScoped<ICountryStrategy, JapanStrategy>();
            services.AddScoped<ICountryStrategy, NorthKoreaStrategy>();
            services.AddScoped<ICountryStrategy, RussiaStrategy>();
            services.AddScoped<ICountryStrategy, SwitzerlandStrategy>();
            services.AddScoped<ICountryStrategy, UnitedStatesStrategy>();
            services.AddScoped<ICountryStrategyFactory, CountryStrategyFactory>();

            return services;
        }
    }
}
