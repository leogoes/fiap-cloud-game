using FIAP.Cloud.Games.Core.DelegatingHandlers;
using FIAP.Cloud.Games.SDK;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System.Reflection;

await Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        hostContext.Configuration = new ConfigurationBuilder()
        .AddUserSecrets(Assembly.GetExecutingAssembly(), true)
        .AddEnvironmentVariables()
        .Build();

        CloudGamesBootstraper.Bootstrap(services, hostContext.Configuration["FIAP_CLOUD_GAME_CORE_HOST"], x =>
        {
            return new AuthenticationHandler(hostContext.Configuration["OPEN_ID_HOST"],
                                             hostContext.Configuration["CLIENT_ID"],
                                             hostContext.Configuration["CLIENT_SECRET"]);
        });

        services.AddMassTransit(cfg =>
        {
            cfg.SetKebabCaseEndpointNameFormatter();

            cfg.AddConsumers(typeof(Program).Assembly);

            cfg.UsingRabbitMq((context, transport) =>
            {
                transport.Host("localhost", "/", h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                });

                transport.UseInMemoryOutbox(context);

                transport.ConfigureEndpoints(context);
            });
        });
    })
    .RunConsoleAsync();