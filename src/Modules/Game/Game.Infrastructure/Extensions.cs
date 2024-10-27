using Game.Domain.RoomAggregate.Entities;
using Game.Domain.UserAggregate.Entities;
using DomainGame = Game.Domain.GameAggregate.Entities.Game;
using Game.Infrastructure.Contexts;
using Game.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Shared.Postgres;
using WorldDomination.Shared.Domain;
using Game.Domain.CountryAggregate.Entities;
using Microsoft.EntityFrameworkCore;
using Game.Infrastructure.Realtime;
using Game.Application.Services;

namespace Game.Infrastructure
{
    public static class Extensions
    {
        public static IServiceCollection AddGameInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<IRepository<City>, CityRepository>();
            services.AddScoped<IRepository<Country>, CountryRepository>();
            services.AddScoped<IRepository<DomainGame>, GameRepository>();
            services.AddScoped<IRepository<Organizer>, OrganizerRepository>();
            services.AddScoped<IRepository<Player>, PlayerRepository>();
            services.AddScoped<IRepository<Room>, RoomRepository>();
            services.AddScoped<IRepository<GameUser>, UserRepository>();


            services.AddPostgres<GameWriteDbContext>();
            services.AddPostgres<GameReadDbContext>(QueryTrackingBehavior.NoTracking);

            services.AddSignalR();
            services.AddSingleton<IRoomFeedHub, RoomFeedHub>();

            return services;
        }
    }
}
