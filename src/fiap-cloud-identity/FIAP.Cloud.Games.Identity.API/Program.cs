using FIAP.Cloud.Games.Identity.API.Configurations.Auth;
using FIAP.Cloud.Games.Identity.API.Configurations.Auth.Policies;
using FIAP.Cloud.Games.Identity.API.Configurations.Documentations;
using FIAP.Cloud.Games.Identity.API.Configurations.MessageBrokers;
using FIAP.Cloud.Games.Identity.API.Endpoints.Users;
using FIAP.Cloud.Games.Identity.Application;
using FIAP.Cloud.Games.Identity.CrossCutting.Security;
using FIAP.Cloud.Games.Identity.Data.Contexts;
using FIAP.Cloud.Games.Identity.Data.Seeds;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

var configuration = new ConfigurationBuilder()
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("DOTNET_ENVIROMENT")}.json", optional: true)
        .AddUserSecrets(Assembly.GetExecutingAssembly(), true)
        .AddEnvironmentVariables()
        .Build();

Log.Logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .MinimumLevel.Debug()
    .CreateLogger();

builder.Host.UseSerilog();

builder.Services.AddAuthorization();

builder.Services.AddEndpointsApiExplorer();

builder.Services.Configure<JwtSettings>(configuration);

builder.Services.AddDbContext<UserContext>(options =>
{
    options.UseMySql(configuration["DB_FIAP_IDENTITY"], new MySqlServerVersion(new Version(8, 0, 21)));
});

builder.Services.ConfigureIdentityEndpoints();
builder.Services.AddCustomAuthentication(configuration);
builder.Services.ConfigureIdentityOptions();
builder.Services.AddCustomPolicies();

builder.Services.AddNSwagConfiguration();

builder.Services.AddMasstransitConfiguration();

ApplicationBootstraper.Bootstrap(builder.Services);

var app = builder.Build();

app.MapDefaultEndpoints();

if (app.Environment.IsDevelopment())
{
    app.UseOpenApi();
    app.UseSwaggerUi();
}

app.UseAuthentication();
app.UseAuthorization();

app.SetUsersEndpoints();

using (IServiceScope scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<UserContext>();

    await UserSeed.UserSeedAsync(services, configuration);
}

app.Run();

