using FIAP.Cloud.Games.Data.Queries;

namespace FIAP.Cloud.Games.API.Configurations
{
    public static class GraphqlConfiguration
    {
        public static void AddGraphqlConfiguration(this IServiceCollection services)
        {
            services.AddGraphQLServer()
                .AddQueryType<GameQuery>()
                .AddProjections()
                .AddFiltering();
        }
    }
}
