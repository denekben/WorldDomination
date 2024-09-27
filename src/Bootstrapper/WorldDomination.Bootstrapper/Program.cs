using AppUser.WebUI;
using Shared;

var builder = WebApplication
    .CreateBuilder(args);

builder.Services
    .AddAppUserModule(builder.Configuration)
    .AddSharedFramework(builder.Configuration);

var app = builder.Build();

app.UseSharedFramework();
app.UseAppUserModule();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapGet("/", ctx => ctx.Response.WriteAsync(":^)"));
});

app.Run();