using FIAP.Cloud.Games.Domain.Games.Rules;

namespace FIAP.Cloud.Games.Application.Games.Requests
{
    public record GameChangeRequest
    {
        public string? Name { get; init; }
        public GameCategoryEnum? Category { get; init; }
        public decimal? Pricing { get; init; }
    }
}
