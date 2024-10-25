using User.Application.Services;
using Microsoft.Extensions.DependencyInjection;
using Shared.Postgres;
using User.Infrastructure.Contexts;
using User.Infrastructure.Repositories;
using User.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using WorldDomination.Shared.Domain;
using User.Domain.Entities;

namespace User.Infrastructure
{
    public static class Extensions
    {
        public static IServiceCollection AddUserInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<IRepository<DomainUser>, UserRepository>();
            services.AddScoped<IRepository<Achievment>, AchievmentRepository>();
            services.AddScoped<IRepository<UserStatus>, UserStatusRepository>();

            services.AddPostgres<UserReadDbContext>(QueryTrackingBehavior.NoTracking);
            services.AddPostgres<UserWriteDbContext>();

            services.AddScoped<IProfileImageService, ImageService>();

            return services;
        }
    }
}
