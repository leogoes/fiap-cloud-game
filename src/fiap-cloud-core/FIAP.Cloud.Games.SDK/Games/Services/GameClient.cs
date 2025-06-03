using FIAP.Cloud.Games.Core.Responses;
using FIAP.Cloud.Games.SDK.Games.Requests;
using FIAP.Cloud.Games.SDK.Games.Responses;
using FIAP.Cloud.Games.SDK.Games.Responses.Core;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;

namespace FIAP.Cloud.Games.SDK.Games.Services
{
    public class GameClient(ILogger<GameClient> logger, IHttpClientFactory httpClientFactory)
    {
        public HttpClient HttpClient => httpClientFactory.CreateClient("CLOUD_GAME_CLIENT");

        public async Task<HttpInternalResponse<GameCreateResponse, ResponseErrorDetail>> CreateAsync(GameCreateRequest request)
        {
            var stringContent = new StringContent(JsonSerializer.Serialize(request));

            try
            {
                var response = await HttpClient.PostAsync("/game", stringContent);

                if (!response.IsSuccessStatusCode)
                {
                    var errors = await response.Content.ReadFromJsonAsync<ResponseErrorDetail>();

                    return new HttpInternalResponse<GameCreateResponse, ResponseErrorDetail>(true, errors!.Details) { StatusCode = response.StatusCode };
                }

                var successResponse = await response.Content.ReadFromJsonAsync<GameCreateResponse>();

                return new HttpInternalResponse<GameCreateResponse, ResponseErrorDetail>(successResponse!) { StatusCode = response.StatusCode };
            }
            catch (Exception)
            {
                return new HttpInternalResponse<GameCreateResponse, ResponseErrorDetail>(true, []) { StatusCode = HttpStatusCode.InternalServerError };
            }
        }

        public async Task<HttpInternalResponse<GameResponse, ResponseErrorDetail>> ChangeAsync(Guid gameId, GameChangeRequest request)
        {
            var stringContent = new StringContent(JsonSerializer.Serialize(request));

            try
            {
                var response = await HttpClient.PostAsync($"/game/{gameId}", stringContent);

                if (!response.IsSuccessStatusCode)
                {
                    var errors = await response.Content.ReadFromJsonAsync<ResponseErrorDetail>();

                    return new HttpInternalResponse<GameResponse, ResponseErrorDetail>(true, errors!.Details) { StatusCode = response.StatusCode };
                }

                var successResponse = await response.Content.ReadFromJsonAsync<GameResponse>();

                return new HttpInternalResponse<GameResponse, ResponseErrorDetail>(successResponse!) { StatusCode = response.StatusCode };
            }
            catch (Exception)
            {
                return new HttpInternalResponse<GameResponse, ResponseErrorDetail>(true, []) { StatusCode = HttpStatusCode.InternalServerError };
            }
        }

        public async Task<HttpInternalResponse<IEnumerable<GameResponse>, ResponseErrorDetail>> FindAllAsync(GameFindRequest request)
        {
            try
            {
                var queryString = new QueryBuilder
                {
                    { "name", request.Name ?? string.Empty },
                    { "pricing", request.Pricing.ToString() },
                    { "category", request.Category.ToString() }
                };

                var response = await HttpClient.GetAsync($"/game{queryString}");

                if (!response.IsSuccessStatusCode)
                {
                    var errors = await response.Content.ReadFromJsonAsync<ResponseErrorDetail>();

                    return new HttpInternalResponse<IEnumerable<GameResponse>, ResponseErrorDetail>(true, errors!.Details) { StatusCode = response.StatusCode };
                }

                var successResponse = await response.Content.ReadFromJsonAsync<IEnumerable<GameResponse>>();

                return new HttpInternalResponse<IEnumerable<GameResponse>, ResponseErrorDetail>(successResponse!) { StatusCode = response.StatusCode };
            }
            catch (Exception)
            {
                return new HttpInternalResponse<IEnumerable<GameResponse>, ResponseErrorDetail>(true, []) { StatusCode = HttpStatusCode.InternalServerError };
            }
        }
    }
}
