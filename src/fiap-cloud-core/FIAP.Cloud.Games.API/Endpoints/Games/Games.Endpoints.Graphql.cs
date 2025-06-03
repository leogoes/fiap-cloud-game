namespace FIAP.Cloud.Games.API.Endpoints.Games
{
    public static class GamesEndpointsGraphql
    {
        public static void SetGamesGraphqlEndpoints(this WebApplication app)
        {
            app.MapGraphQL("/graphql").WithTags("GraphQL");
        }
    }
}
