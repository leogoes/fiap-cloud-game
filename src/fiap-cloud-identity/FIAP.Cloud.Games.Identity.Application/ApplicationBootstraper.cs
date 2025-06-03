using FIAP.Cloud.Games.Identity.Application.Users.Events.Handlers;
using FIAP.Cloud.Games.Identity.Application.Users.Services;
using Microsoft.Extensions.DependencyInjection;

namespace FIAP.Cloud.Games.Identity.Application
{
    public static class ApplicationBootstraper
    {
        public static void Bootstrap(this IServiceCollection services)
        {
            services.AddScoped<UserAppService>();
            services.AddScoped<UserEventHandler>();
        }
    }
}
