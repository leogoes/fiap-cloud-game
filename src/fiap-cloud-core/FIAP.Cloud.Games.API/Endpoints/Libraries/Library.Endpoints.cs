using FIAP.Cloud.Games.API.Configurations.Auth.Policies;
using FIAP.Cloud.Games.Application.Libraries.Requests;
using FIAP.Cloud.Games.Application.Libraries.Responses;
using FIAP.Cloud.Games.Application.Libraries.Services;
using FIAP.Cloud.Games.Core.Responses.Https;
using Microsoft.AspNetCore.Mvc;

namespace FIAP.Cloud.Games.API.Endpoints.Libraries
{
    public static class LibraryEndpoints
    {
        public static void SetLibrariesEndpoints(this WebApplication app)
        {
            app.MapPost("/library", async ([FromBody] LibraryCreateRequest request, [FromServices] LibraryService service) =>
            {
                var result = await service.CreateAsync(request);

                if (result.Conflict)
                    return Results.Conflict(new Response409Error());

                if (result.NotFound)
                    return Results.NotFound(new Response404Error());

                if (result.Error)
                    return Results.BadRequest(new Response400Error<LibraryResponse>(result));

                return Results.Ok(result.Content);
            })
            .Produces<Response409Error>(StatusCodes.Status409Conflict)
            .Produces<Response404Error>(StatusCodes.Status404NotFound)
            .Produces<Response400Error>(StatusCodes.Status400BadRequest)
            .RequireAuthorization(PoliciesConst.CanCreateLibraries)
            .WithTags("libraries");

            app.MapGet("/library", async ([FromQuery(Name = "user_id")] Guid? UserId, [FromServices] LibraryService service) =>
            {
                var result = await service.FindAsync(new LibraryFindRequest(UserId, null));


                if (result.Conflict)
                    return Results.Conflict(new Response409Error());

                if (result.NotFound)
                    return Results.NotFound(new Response404Error());

                if (result.Error)
                    return Results.BadRequest(new Response400Error<LibraryResponse>(result));

                return Results.Ok(result.Content);
            })
            .Produces<Response409Error>(StatusCodes.Status409Conflict)
            .Produces<Response404Error>(StatusCodes.Status404NotFound)
            .Produces<Response400Error>(StatusCodes.Status400BadRequest)
            .RequireAuthorization(PoliciesConst.CanViewLibraries)
            .WithTags("libraries");
        }
    }
}
