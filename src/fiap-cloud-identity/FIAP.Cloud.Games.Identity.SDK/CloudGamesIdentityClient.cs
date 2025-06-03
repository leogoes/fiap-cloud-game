using FIAP.Cloud.Games.Core.Responses;
using FIAP.Cloud.Games.Core.Security.Tokens;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;

namespace FIAP.Cloud.Games.Identity.SDK
{
    public class CloudGamesIdentityClient(ILogger<CloudGamesIdentityClient> logger, IHttpClientFactory httpClientFactory)
    {
        public HttpClient HttpClient => httpClientFactory.CreateClient("CLOUD_IDENTITY_CLIENT");

        public async Task<HttpInternalResponse<AuthenticationToken, ResponseErrorDetail>> FindTokenAsync(string username, string password)
        {
            var stringContent = new StringContent(JsonSerializer.Serialize(new UserAuthenticateRequest { Email= username, Password = password }));

            try
            {
                var response = await HttpClient.PostAsync("/realms/fiap_cloud_games/protocol/openid-connect/token", stringContent);

                if (!response.IsSuccessStatusCode)
                {
                    var errors = await response.Content.ReadFromJsonAsync<ResponseErrorDetail>();

                    return new HttpInternalResponse<AuthenticationToken, ResponseErrorDetail>(true, errors!.Details) { StatusCode = response.StatusCode };
                }

                var successResponse = await response.Content.ReadFromJsonAsync<AuthenticationToken>();

                return new HttpInternalResponse<AuthenticationToken, ResponseErrorDetail>(successResponse!) { StatusCode = response.StatusCode };
            }
            catch (Exception)
            {
                return new HttpInternalResponse<AuthenticationToken, ResponseErrorDetail>(true, []) { StatusCode = HttpStatusCode.InternalServerError };
            }
        }
    }
}
