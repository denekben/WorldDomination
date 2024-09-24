using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Shared.Events;
using Shared.Exceptions;
using Shared.Messaging;
using Shared.Postgres;
using System.Reflection;

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
            services.AddControllers();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddMediatR(configuration =>
            {
                configuration.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            });
            services.AddSwaggerGen(swagger =>
            {
                swagger.EnableAnnotations();
                swagger.CustomSchemaIds(x => x.FullName);
                swagger.SwaggerDoc(ApiVersion, new OpenApiInfo
                {
                    Title = ApiTitle,
                    Version = ApiVersion
                });
            });

            return services;
        }

        public static IApplicationBuilder UseSharedFramework(this IApplicationBuilder app)
        {
            app.UseErrorHandling();
            app.UseSwagger();
            app.UseReDoc(reDoc =>
            {
                reDoc.RoutePrefix = "docs";
                reDoc.SpecUrl($"/swagger/{ApiVersion}/swagger.json");
                reDoc.DocumentTitle = ApiTitle;
            });
            app.UseRouting();

            return app;
        }
    }
}
