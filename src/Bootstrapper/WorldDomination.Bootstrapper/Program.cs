using AppUser.WebUI;
using Microsoft.OpenApi.Models;
using Shared;

var builder = WebApplication
    .CreateBuilder(args);

// Modules
builder.Services
    .AddAppUserModule(builder.Configuration)
    .AddSharedFramework(builder.Configuration);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "WorldDomination", Version = "v1" }));

var app = builder.Build();


// Modules
app.UseSharedFramework();
app.UseAppUserModule();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();