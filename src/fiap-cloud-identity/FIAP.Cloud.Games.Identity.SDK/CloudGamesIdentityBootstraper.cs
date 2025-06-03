using Microsoft.Extensions.DependencyInjection;

namespace FIAP.Cloud.Games.Identity.SDK
{
    public class CloudGamesIdentityBootstraper
    {
        public static void Bootstrap(IServiceCollection services, string host)
        {
            services.AddHttpClient("CLOUD_IDENTITY_CLIENT", config =>
            {
                config.BaseAddress = new Uri(host);
            });

            services.AddScoped<CloudGamesIdentityClient>();
        }
    }
}
