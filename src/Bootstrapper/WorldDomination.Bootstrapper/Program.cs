using User.WebUI;
using Identity.WebUI;
using Shared;

var builder = WebApplication
    .CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Modules //
builder.Services
    .AddIdentityModule(builder.Configuration)
    .AddUserModule(builder.Configuration)
    .AddSharedFramework(builder.Configuration);
/////////////

var app = builder.Build();

// Modules //
app.UseIdentityModule();
app.UseUserModule();
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