using User.Application.Services;
using User.Domain.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Shared.Postgres;
using User.Infrastructure.Contexts;
using User.Infrastructure.Repositories;
using User.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

namespace User.Infrastructure
{
    public static class Extensions
    {
        public static IServiceCollection AddUserInfrastructure(this IServiceCollection services, Microsoft.Extensions.Configuration.IConfiguration configuration)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IAchievmentRepository, AchievmentRepository>();
            services.AddScoped<IUserStatusRepository, UserStatusRepository>();

            services.AddPostgres<UserReadDbContext>(QueryTrackingBehavior.NoTracking);
            services.AddPostgres<UserWriteDbContext>();

            services.AddScoped<IProfileImageService, ProfileImageService>();

            return services;
        }
    }
}
