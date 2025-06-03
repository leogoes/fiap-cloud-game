using FIAP.Cloud.Games.Core.Responses;
using FIAP.Cloud.Games.SDK.Libraries.Requests;
using FIAP.Cloud.Games.SDK.Libraries.Responses;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using static System.Net.Mime.MediaTypeNames;

namespace FIAP.Cloud.Games.SDK.Libraries.Services
{
    public class LibraryClient(ILogger<LibraryClient> logger, IHttpClientFactory httpClientFactory)
    {
        public HttpClient HttpClient => httpClientFactory.CreateClient("CLOUD_GAME_CLIENT");

        public async Task<HttpInternalResponse<LibraryCreateResponse, ResponseErrorDetail>> CreateAsync(LibraryCreateRequest request)
        {
            var stringContent = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, Application.Json);

            try
            {
                var response = await HttpClient.PostAsync("/library", stringContent);

                if (!response.IsSuccessStatusCode)
                {
                    var errors = await response.Content.ReadFromJsonAsync<ResponseErrorDetail>();

                    return new HttpInternalResponse<LibraryCreateResponse, ResponseErrorDetail>(true, errors!.Details) { StatusCode = response.StatusCode };
                }

                var successResponse = await response.Content.ReadFromJsonAsync<LibraryCreateResponse>();

                return new HttpInternalResponse<LibraryCreateResponse, ResponseErrorDetail>(successResponse!) { StatusCode = response.StatusCode };
            }
            catch (Exception)
            {
                return new HttpInternalResponse<LibraryCreateResponse, ResponseErrorDetail>(true, []) { StatusCode = HttpStatusCode.InternalServerError };
            }
        }

        public async Task<HttpInternalResponse<IEnumerable<LibraryResponse>, ResponseErrorDetail>> FindAllAsync(LibraryFindRequest request)
        {
            try
            {
                var queryString = new QueryBuilder
                {
                    { "user_id", request.UserId.ToString() },
                };

                var response = await HttpClient.GetAsync($"/library{queryString}");

                if (!response.IsSuccessStatusCode)
                {
                    var errors = await response.Content.ReadFromJsonAsync<ResponseErrorDetail>();

                    return new HttpInternalResponse<IEnumerable<LibraryResponse>, ResponseErrorDetail>(true, errors!.Details) { StatusCode = response.StatusCode };
                }

                var successResponse = await response.Content.ReadFromJsonAsync<IEnumerable<LibraryResponse>>();

                return new HttpInternalResponse<IEnumerable<LibraryResponse>, ResponseErrorDetail>(successResponse!) { StatusCode = response.StatusCode };
            }
            catch (Exception)
            {
                return new HttpInternalResponse<IEnumerable<LibraryResponse>, ResponseErrorDetail>(true, []) { StatusCode = HttpStatusCode.InternalServerError };
            }
        }
    }
}
