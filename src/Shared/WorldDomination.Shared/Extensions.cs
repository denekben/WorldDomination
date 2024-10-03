using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Shared.Events;
using Shared.Exceptions;
using Shared.Messaging;
using Shared.Postgres;
using System;

namespace Shared
{
    public static class Extensions
    {
        private const string ApiTitle = "WorldDomination API";
        private const string ApiVersion = "v1";

        public static IServiceCollection AddSharedFramework(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddErrorHandling();
            services.AddEvents();
            services.AddMessaging();
            services.AddPostgres(configuration);
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddMediatR(configuration => 
                configuration.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies())
            );
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(options =>
                options.SwaggerDoc("v1", new OpenApiInfo { Title = ApiTitle, Version = ApiVersion }));

            return services;
        }

        public static IApplicationBuilder UseSharedFramework(this IApplicationBuilder app)
        {
            app.UseErrorHandling();

            return app;
        }
    }
}
