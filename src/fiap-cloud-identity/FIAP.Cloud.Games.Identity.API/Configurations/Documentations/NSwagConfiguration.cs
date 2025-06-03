using NSwag.Generation.Processors.Security;
using NSwag;

namespace FIAP.Cloud.Games.Identity.API.Configurations.Documentations
{
    public static class NSwagConfiguration
    {
        public static void AddNSwagConfiguration(this IServiceCollection services)
        {
            services.AddOpenApiDocument(config =>
            {
                config.PostProcess = document =>
                {
                    document.Info.Version = "v1";
                    document.Info.Title = "Fiap.Cloud.Games.Identity";
                    document.Info.Description = "API de identidade para a Fiap Cloud Games";
                    document.Info.Contact = new OpenApiContact
                    {
                        Name = "Leonardo Goes",
                        Email = "leonardo.goes@gmail.com"
                    };
                    document.Info.License = new OpenApiLicense
                    {
                        Name = "MIT",
                        Url = "https://opensource.org/licenses/MIT"
                    };
                };

                config.AddSecurity("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Bearer",
                    In = OpenApiSecurityApiKeyLocation.Header,
                    Type = OpenApiSecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    Description = "JWT Authorization header using the Bearer scheme."
                });

                config.OperationProcessors.Add(new AspNetCoreOperationSecurityScopeProcessor("Bearer"));
            });
        }
    }
}
