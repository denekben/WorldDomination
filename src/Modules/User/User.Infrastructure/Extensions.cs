using User.Application.Services;
using Microsoft.Extensions.DependencyInjection;
using Shared.Postgres;
using User.Infrastructure.Contexts;
using User.Infrastructure.Repositories;
using User.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using WorldDomination.Shared.Domain;
using User.Domain.Entities;
using User.Domain.Repositories;

namespace User.Infrastructure
{
    public static class Extensions
    {
        public static IServiceCollection AddUserInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IAchievmentRepository, AchievmentRepository>();
            services.AddScoped<IUserStatusRepository, UserStatusRepository>();

            services.AddPostgres<UserReadDbContext>(QueryTrackingBehavior.NoTracking);
            services.AddPostgres<UserWriteDbContext>();

            services.AddScoped<IProfileImageService, ImageService>();

            return services;
        }
    }
}
