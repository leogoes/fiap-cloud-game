using FIAP.Cloud.Games.Application.Games.Abstractions;
using FIAP.Cloud.Games.Application.Libraries.Abstractions;
using FIAP.Cloud.Games.Data.Contexts;
using FIAP.Cloud.Games.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace FIAP.Cloud.Games.Data
{
    public class DataBootstraper
    {
        public static void Bootstrap(IServiceCollection services)
        {
            services.AddScoped<IGameRepository, GameRepository>();
            services.AddScoped<ILibraryRepository, LibraryRepository>();
            services.AddScoped<GameContext>();
            services.AddScoped<LibraryContext>();
        }
    }
}
