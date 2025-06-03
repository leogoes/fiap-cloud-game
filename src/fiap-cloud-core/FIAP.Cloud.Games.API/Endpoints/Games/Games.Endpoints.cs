using FIAP.Cloud.Games.API.Configurations.Auth.Policies;
using FIAP.Cloud.Games.Application.Games.Requests;
using FIAP.Cloud.Games.Application.Games.Responses;
using FIAP.Cloud.Games.Application.Games.Responses.Core;
using FIAP.Cloud.Games.Application.Games.Services;
using FIAP.Cloud.Games.Core.Responses.Https;
using FIAP.Cloud.Games.Domain.Games.Rules;
using Microsoft.AspNetCore.Mvc;

namespace FIAP.Cloud.Games.API.Endpoints.Games
{
    public static class GamesEndpoints
    {
        public static void SetGamesEndpoints(this WebApplication app)
        {
            app.MapPost("/games", async ([FromBody] GameCreateRequest request, GameService gameService1) =>
            {
                var result = await gameService1.CreateAsync(request);

                if (result.Conflict)
                    return Results.Conflict(new Response409Error());

                if (result.NotFound)
                    return Results.NotFound(new Response404Error());

                if (result.Error)
                    return Results.BadRequest(new Response400Error<GameCreateResponse>(result));

                return Results.Ok(result.Content);
            })
            .Produces<Response409Error>(StatusCodes.Status409Conflict)
            .Produces<Response404Error>(StatusCodes.Status404NotFound)
            .Produces<Response400Error>(StatusCodes.Status400BadRequest)
            .Produces<Response401Error>(StatusCodes.Status401Unauthorized)
            .Produces<Response403Error>(StatusCodes.Status403Forbidden)
            .RequireAuthorization(PoliciesConst.CanCreateGames)
            .WithTags("games");

            app.MapPut("/games/{game_id:guid}", async([FromRoute(Name = "game_id")] Guid GameId, [FromBody] GameChangeRequest request, [FromServices] GameService gameService2) =>
            {
                var result = await gameService2.ChangeAsync(GameId, request);

                if (result.Conflict)
                    return Results.Conflict(new Response409Error());

                if (result.NotFound)
                    return Results.NotFound(new Response404Error());

                if (result.Error)
                    return Results.BadRequest(new Response400Error<GameResponse>(result));

                return Results.Ok(result.Content);
            })
            .Produces<Response409Error>(StatusCodes.Status409Conflict)
            .Produces<Response404Error>(StatusCodes.Status404NotFound)
            .Produces<Response400Error>(StatusCodes.Status400BadRequest)
            .Produces<Response401Error>(StatusCodes.Status401Unauthorized)
            .Produces<Response403Error>(StatusCodes.Status403Forbidden)
            .RequireAuthorization(PoliciesConst.CanChangeGames)
            .WithTags("games");

            app.MapGet("/games", async([FromQuery(Name = "name")] string? Name,
                                       [FromQuery(Name = "pricing")] decimal? Pricing,
                                       [FromQuery(Name = "category")] GameCategoryEnum? Category,
                                       [FromServices] GameService service) =>
            {
                var result = await service.FindAllAsync(new GameFindRequest(Name,Pricing,Category));

                if (result.Conflict)
                    return Results.Conflict(new Response409Error());

                if (result.NotFound)
                    return Results.NotFound(new Response404Error());

                if (result.Error)
                    return Results.BadRequest(new Response400Error<IEnumerable<GameResponse>>(result));

                return Results.Ok(result.Content);
            })
            .Produces<Response409Error>(StatusCodes.Status409Conflict)
            .Produces<Response404Error>(StatusCodes.Status404NotFound)
            .Produces<Response400Error>(StatusCodes.Status400BadRequest)
            .Produces<Response401Error>(StatusCodes.Status401Unauthorized)
            .Produces<Response403Error>(StatusCodes.Status403Forbidden)
            .RequireAuthorization(PoliciesConst.CanViewGames)
            .WithTags("games");
        }
    }
}
