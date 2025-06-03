using FIAP.Cloud.Games.API.Configurations.Auth.Policies;
using FIAP.Cloud.Games.Core.Responses.Https;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace FIAP.Cloud.Games.API.Configurations.Auth
{
    public static class AuthenticationConfiguration
    {
        public static void AddCustomAuthentication(this IServiceCollection services, IConfigurationRoot configuration)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                 .AddJwtBearer(AuthenticationSchemesConst.FIAP_IDENTITY, jwtOptions =>
                 {
                     jwtOptions.RequireHttpsMetadata = true;
                     jwtOptions.SaveToken = true;
                     jwtOptions.Authority = configuration["AUTHORITY"];
                     jwtOptions.Audience = configuration["AUDIENCE"];
                     jwtOptions.TokenValidationParameters = new TokenValidationParameters
                     {
                         ValidateIssuer = true,
                         ValidateAudience = true,
                         ValidateIssuerSigningKey = true,
                         IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration["SECRET"])),
                         ValidAudiences = configuration.GetSection("AUTHENTICATION_AUDIENCES").Get<string[]>(),
                         ValidIssuers = configuration.GetSection("VALID_ISSUER").Get<string[]>(),
                         RoleClaimType = "role",
                     };
                     jwtOptions.Events = new JwtBearerEvents
                     {
                         OnForbidden = async (context) =>
                         {
                             if (!context.Response.HasStarted)
                             {
                                 context.Response.StatusCode = 403;
                                 await context.HttpContext.Response.WriteAsJsonAsync(new Response403Error());
                             }
                         },
                         OnChallenge = async (context) =>
                         {
                             if (!context.Response.HasStarted)
                             {
                                 context.Response.StatusCode = 401;
                                 context.Response.ContentType = "application/json";
                                 await context.HttpContext.Response.WriteAsJsonAsync(new Response401Error());
                                 context.HandleResponse();
                             }
                         }
                     };

                     jwtOptions.RequireHttpsMetadata = false;
                     jwtOptions.MapInboundClaims = false;
                 })
                 .AddJwtBearer(AuthenticationSchemesConst.KEYCLOAK_IDENTITY, o =>
                 {
                     o.RequireHttpsMetadata = false;
                     o.Audience = configuration["KEYCLOAK_AUDIENCE"];
                     o.MetadataAddress = configuration["KEYCLOAK_METADATA"]!;
                     o.TokenValidationParameters = new TokenValidationParameters
                     {
                         ValidIssuer = configuration["KEYCLOAK_VALID_ISSUER"]
                     };
                 });
        }
    }
}
