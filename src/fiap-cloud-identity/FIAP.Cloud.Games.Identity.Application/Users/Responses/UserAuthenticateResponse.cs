using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace FIAP.Cloud.Games.Identity.Application.Users.Responses
{
    public record UserAuthenticateResponse
    {
        [DataMember(Name = "access_token"), JsonPropertyName("access_token")]
        public required string AccessToken { get; init; }

        [DataMember(Name = "expires_in"), JsonPropertyName("expires_in")]
        public required double ExpiresIn { get; init; }
    }
}
