using FIAP.Cloud.Games.Application.Games.Services;
using FIAP.Cloud.Games.Application.Libraries.Services;
using Microsoft.Extensions.DependencyInjection;

namespace FIAP.Cloud.Games.Application
{
    public class ApplicationBootstraper
    {
        public static void Bootstrap(IServiceCollection services)
        {
            services.AddScoped<GameService>();
            services.AddScoped<LibraryService>();
        }
    }
}
