using FIAP.Cloud.Games.Core.Responses.Https;
using FIAP.Cloud.Games.Identity.API.Configurations.Auth.Policies;
using FIAP.Cloud.Games.Identity.Application.Users.Requests;
using FIAP.Cloud.Games.Identity.Application.Users.Responses;
using FIAP.Cloud.Games.Identity.Application.Users.Services;
using FIAP.Cloud.Games.Identity.Data.Models;
using FIAP.Cloud.Games.Identity.Data.Rules;
using Microsoft.AspNetCore.Http;

namespace FIAP.Cloud.Games.Identity.API.Endpoints.Users
{
    public static class UsersEndpoints
    {
        public static void SetUsersEndpoints(this WebApplication app)
        {
            app.MapPost("/user", async(UserCreateRequest request, UserAppService userService) =>
            {
                var result = await userService.UserCreateAsync(request);

                if (result.Conflict)
                    return Results.Conflict(new Response409Error());

                if (result.NotFound)
                    return Results.NotFound(new Response404Error());

                if (result.Error)
                    return Results.BadRequest(new Response400Error<CurrentUser>(result));

                return Results.Ok(result.Content);
            })
            .Produces<Response409Error>(StatusCodes.Status409Conflict)
            .Produces<Response404Error>(StatusCodes.Status404NotFound)
            .Produces<Response400Error>(StatusCodes.Status400BadRequest)
            .WithName("Users")
            .WithTags("users");

            app.MapPost("/role", async (UserRoleAssingRequest request, HttpContext httpContext, UserAppService userService) =>
            {
                var result = await userService.UserRoleAssignAsync(request);

                if (result.Conflict)
                    return Results.Conflict(new Response409Error());

                if (result.NotFound)
                    return Results.NotFound(new Response404Error());

                if (result.Error)
                    return Results.BadRequest(new Response400Error<CurrentUser>(result));

                return Results.Ok(result.Content);
            })
            .Produces<Response409Error>(StatusCodes.Status409Conflict)
            .Produces<Response404Error>(StatusCodes.Status404NotFound)
            .Produces<Response400Error>(StatusCodes.Status400BadRequest)
            .RequireAuthorization(PoliciesConst.Admin)
            .WithName("Roles")
            .WithTags("roles");

            app.MapPost("/user/token", async (UserAuthenticateRequest request, UserAppService userService) =>
            {
                var result = await userService.UserAuthenticateAsync(request);

                if (result.Conflict)
                    return Results.Conflict(new Response409Error());

                if (result.NotFound)
                    return Results.NotFound(new Response404Error());

                if (result.Error)
                    return Results.BadRequest(new Response400Error<UserAuthenticateResponse>(result));

                return Results.Ok(result.Content);
            })
            .Produces<UserAuthenticateResponse>(StatusCodes.Status200OK)
            .Produces<Response409Error>(StatusCodes.Status409Conflict)
            .Produces<Response404Error>(StatusCodes.Status404NotFound)
            .Produces<Response400Error>(StatusCodes.Status400BadRequest)
            .WithName("Authenticate")
            .WithTags("users");
        }
    }
}
