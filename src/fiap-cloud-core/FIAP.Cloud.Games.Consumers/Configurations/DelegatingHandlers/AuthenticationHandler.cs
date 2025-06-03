using FIAP.Cloud.Games.Core.Security.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace FIAP.Cloud.Games.Core.DelegatingHandlers
{
    public class AuthenticationHandler(string host, string clientId, string clientSecret) : DelegatingHandler
    {
        public string Host { get; set; } = host;
        public string ClientId { get; set; } = clientId;
        public string ClientSecret { get; set; } = clientSecret;

        protected override async Task<HttpResponseMessage> SendAsync(
         HttpRequestMessage request,
         CancellationToken cancellationToken)
        {
            var authToken = await GetAccessTokenAsync();

            request.Headers.Authorization = new AuthenticationHeaderValue(
                JwtBearerDefaults.AuthenticationScheme,
                authToken.AccessToken);

            var httpResponseMessage = await base.SendAsync(
                request,
                cancellationToken);

            httpResponseMessage.EnsureSuccessStatusCode();

            return httpResponseMessage;
        }

        private async Task<AuthenticationToken> GetAccessTokenAsync()
        {
            var @params = new KeyValuePair<string, string>[]
            {
                new("client_id", ClientId),
                new("client_secret", ClientSecret),
                new("grant_type", "client_credentials")
            };

            var content = new FormUrlEncodedContent(@params);

            var authRequest = new HttpRequestMessage(
                HttpMethod.Post,
                new Uri(Host))
            {
                Content = content
            };

            var response = await base.SendAsync(authRequest, CancellationToken.None);

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<AuthenticationToken>() ??
                   throw new ApplicationException();
        }
    }
}
