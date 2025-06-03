using FIAP.Cloud.Games.Core.Exceptions;
using System.Text.Json.Serialization;

namespace FIAP.Cloud.Games.Core.Responses
{
    public class ResponseErrorDetail(string slug, string message, IEnumerable<ErrorDetail>? errors = null)
    {
        [JsonPropertyName("slug")]
        public string Slug { get; set; } = slug;

        [JsonPropertyName("message")]
        public string Message { get; set; } = message;

        [JsonPropertyName("details")]
        public IEnumerable<ErrorDetail> Details { get; set; } = errors ?? [];
    }
}
