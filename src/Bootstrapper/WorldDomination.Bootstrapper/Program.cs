using AppUser.WebUI;
using Shared;

var builder = WebApplication
    .CreateBuilder(args);

// Modules
builder.Services
    .AddAppUserModule(builder.Configuration)
    .AddSharedFramework(builder.Configuration);

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