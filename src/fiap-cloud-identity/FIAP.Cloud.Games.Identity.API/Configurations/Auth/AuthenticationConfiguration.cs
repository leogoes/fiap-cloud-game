using FIAP.Cloud.Games.Identity.CrossCutting.Exceptions.Rules;
using FIAP.Cloud.Games.Identity.Data.Contexts;
using FIAP.Cloud.Games.Identity.Data.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace FIAP.Cloud.Games.Identity.API.Configurations.Auth
{
    public static class AuthenticationConfiguration
    {
        public static void AddCustomAuthentication(this IServiceCollection services, IConfigurationRoot configuration)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(jwtOptions =>
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

                    jwtOptions.RequireHttpsMetadata = false;
                    jwtOptions.MapInboundClaims = false;
                });
        }

        public static void ConfigureIdentityOptions(this IServiceCollection services)
        {
            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequiredLength = 8;
                options.Password.RequiredUniqueChars = 1;
            });
        }

        public static void ConfigureIdentityEndpoints(this IServiceCollection services)
        {
            services.AddIdentityApiEndpoints<CurrentUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<UserContext>()
                .AddDefaultTokenProviders()
                .AddErrorDescriber<IdentityMessages>();
        }
    }
}
