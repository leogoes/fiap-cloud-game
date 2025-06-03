using MassTransit;

namespace FIAP.Cloud.Games.API.Configurations.MessageBrokers
{
    public static class MasstransitConfiguration
    {
        public static void AddMasstransitConfiguration(this IServiceCollection services)
        {
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
        }
    }
}
