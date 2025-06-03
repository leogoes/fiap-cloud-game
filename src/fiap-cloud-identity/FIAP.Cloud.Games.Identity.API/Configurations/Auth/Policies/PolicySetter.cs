using FIAP.Cloud.Games.Identity.Data.Rules;
using Microsoft.AspNetCore.Authorization;

namespace FIAP.Cloud.Games.Identity.API.Configurations.Auth.Policies
{
    public static class PolicySetter
    {
        public static void AddCustomPolicies(this AuthorizationOptions options)
        {
            options.AddPolicy(PoliciesConst.Admin, policy => policy.RequireRole(IdentityRolesConst.Admin.Key));
            options.AddPolicy(PoliciesConst.User, policy => policy.RequireRole(IdentityRolesConst.User.Key));
        }   
    }
}
