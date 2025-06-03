using Microsoft.AspNetCore.Http;

namespace FIAP.Cloud.Games.API.Configurations.Auth.Policies
{
    public static class AuthenticationSchemesConst
    {
        public const string FIAP_IDENTITY = "FIAP_IDENTITY";
        public const string KEYCLOAK_IDENTITY = "KEYCLOAK_IDENTITY";
    }
}
