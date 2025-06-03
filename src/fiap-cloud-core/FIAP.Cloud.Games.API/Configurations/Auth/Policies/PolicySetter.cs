using FIAP.Cloud.Games.API.Configurations.Auth.Roles;
using Microsoft.AspNetCore.Authorization;

namespace FIAP.Cloud.Games.API.Configurations.Auth.Policies
{
    public static class PolicySetter
    {
        public static void AddCustomPolicies(this AuthorizationOptions options)
        {
            options.AddPolicy(PoliciesConst.CanCreateGames, policy => policy
            .RequireRole(RolesConst.Admin)
            .AddAuthenticationSchemes(AuthenticationSchemesConst.FIAP_IDENTITY));

            options.AddPolicy(PoliciesConst.CanViewGames, policy => policy
            .RequireRole(RolesConst.Admin, RolesConst.User)
            .AddAuthenticationSchemes(AuthenticationSchemesConst.FIAP_IDENTITY));

            options.AddPolicy(PoliciesConst.CanChangeGames, policy => policy
            .RequireRole(RolesConst.Admin)
            .AddAuthenticationSchemes(AuthenticationSchemesConst.FIAP_IDENTITY));

            options.AddPolicy(PoliciesConst.CanViewLibraries, policy => policy
            .RequireRole(RolesConst.Admin, RolesConst.User)
            .AddAuthenticationSchemes(AuthenticationSchemesConst.FIAP_IDENTITY)); 

            options.AddPolicy(PoliciesConst.CanChangeLibraries, policy => policy
            .RequireRole(RolesConst.Admin, RolesConst.User)
            .AddAuthenticationSchemes(AuthenticationSchemesConst.FIAP_IDENTITY));

            options.AddPolicy(PoliciesConst.CanCreateLibraries, policy => policy
            .RequireRole(RolesConst.Admin)
            .AddAuthenticationSchemes(AuthenticationSchemesConst.KEYCLOAK_IDENTITY));
        }
    }
}
