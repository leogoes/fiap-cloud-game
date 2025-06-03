using System.Text.Json.Serialization;

namespace FIAP.Cloud.Games.Core.Exceptions
{
    public class ErrorDetail
    {
        [JsonPropertyName("slug")]
        public string Slug { get; set; }

        [JsonPropertyName("message")]
        public string Message { get; set; }

        [JsonPropertyName("location")]
        public string Location { get; set; }

        [JsonPropertyName("field")]
        public string Field { get; set; }
    }
}
