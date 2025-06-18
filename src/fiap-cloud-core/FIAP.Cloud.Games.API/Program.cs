using FIAP.Cloud.Games.API.Configurations;
using FIAP.Cloud.Games.API.Configurations.Auth;
using FIAP.Cloud.Games.API.Configurations.Auth.Policies;
using FIAP.Cloud.Games.API.Configurations.Databases;
using FIAP.Cloud.Games.API.Configurations.Documentations;
using FIAP.Cloud.Games.API.Configurations.MessageBrokers;
using FIAP.Cloud.Games.API.Configurations.Middlewares;
using FIAP.Cloud.Games.API.Endpoints.Games;
using FIAP.Cloud.Games.API.Endpoints.Libraries;
using FIAP.Cloud.Games.Application;
using FIAP.Cloud.Games.Core.Responses.Https;
using FIAP.Cloud.Games.Data;
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

Log.Logger = SerilogConfiguration.GetSerilogConfiguration();

builder.Host.UseSerilog();

builder.Services.AddGraphqlConfiguration();

builder.Services.AddNSwagConfiguration();

builder.Services.AddMasstransitConfiguration();

builder.Services.AddMongodbConfiguration(configuration);

builder.Services.AddMysqlConfiguration(configuration);

builder.Services.AddAuthorization();

builder.Services.AddCustomAuthentication(configuration);

builder.Services.AddCustomPolicies();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddCustomPolicies();

ApplicationBootstraper.Bootstrap(builder.Services);

DataBootstraper.Bootstrap(builder.Services);

var app = builder.Build();

app.MapDefaultEndpoints();

app.UseMiddleware<GlobalErrorMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseOpenApi();
    app.UseSwaggerUi();
}

app.UseAuthentication();
app.UseAuthorization();

app.SetGamesEndpoints();
app.SetGamesGraphqlEndpoints();
app.SetLibrariesEndpoints();

app.UseStatusCodePages(async statusCodeContext =>
{
    var response = statusCodeContext.HttpContext.Response;

    switch (response.StatusCode)
    {
        case 404:
            response.StatusCode = 404;
            await response.WriteAsJsonAsync(new Response404Error());
            break;
    }
});

app.Run();

