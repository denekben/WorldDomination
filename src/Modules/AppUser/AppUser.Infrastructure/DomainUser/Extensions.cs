using AppUser.Application.Services;
using AppUser.Domain.Repositories;
using AppUser.Infrastructure.DomainUser.Contexts;
using AppUser.Infrastructure.DomainUser.Repositories;
using AppUser.Infrastructure.DomainUser.Services;
using Microsoft.Extensions.DependencyInjection;
using Shared.Postgres;

namespace AppUser.Infrastructure.DomainUser
{
    public static class Extensions
    {
        public static IServiceCollection AddUserDAL(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IAchievmentRepository, AchievmentRepository>();
            services.AddScoped<IActivityStatusRepository, ActivityStatusRepository>();

            services.AddPostgres<UserReadDbContext>();
            services.AddPostgres<UserWriteDbContext>();

            services.AddScoped<IProfileImageService, ProfileImageService>();

            return services;
        }
    }
}
