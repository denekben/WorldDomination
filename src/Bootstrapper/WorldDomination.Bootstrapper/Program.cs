using Identity.WebUI;
using Shared;
using User.WebUI;
using Game.WebUI;

var builder = WebApplication
    .CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Modules //
builder.Services
    .AddIdentityModule(builder.Configuration)
    .AddUserModule()
    .AddGameModule()
    .AddSharedFramework(builder.Configuration);
/////////////

var app = builder.Build();

// Modules //
app.UseSharedFramework();
/////////////

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();