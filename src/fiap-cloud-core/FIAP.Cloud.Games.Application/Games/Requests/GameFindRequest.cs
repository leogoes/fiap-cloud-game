using FIAP.Cloud.Games.Domain.Games.Rules;

namespace FIAP.Cloud.Games.Application.Games.Requests
{
    public record GameFindRequest
    {
        public GameFindRequest(string? name, decimal? pricing, GameCategoryEnum? category)
        {
            Name = name;
            Pricing = pricing;
            Category = category;
        }

        public string? Name { get; init; }
        public decimal? Pricing { get; init; }
        public GameCategoryEnum? Category { get; init; }
    }
}
