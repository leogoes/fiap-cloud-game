using FIAP.Cloud.Games.SDK.Games.Services;
using FIAP.Cloud.Games.SDK.Libraries.Services;
using Microsoft.Extensions.DependencyInjection;

namespace FIAP.Cloud.Games.SDK
{
    public class CloudGamesBootstraper
    {
        public static void Bootstrap(IServiceCollection services, string host)
        {
            services.AddHttpClient("CLOUD_GAME_CLIENT", config =>
            {
                config.BaseAddress = new Uri(host);
            });

            services.AddScoped<CloudGamesClient>();
            services.AddScoped<GameClient>();
            services.AddScoped<LibraryClient>();
        }
        
        public static void Bootstrap(IServiceCollection services, string host, Func<IServiceProvider, DelegatingHandler> configureHandler)
        {
            services.AddHttpClient("CLOUD_GAME_CLIENT", config =>
            {
                config.BaseAddress = new Uri(host);
            }).AddHttpMessageHandler(configureHandler);

            services.AddScoped<CloudGamesClient>();
            services.AddScoped<GameClient>();
            services.AddScoped<LibraryClient>();
        }
    }
}
