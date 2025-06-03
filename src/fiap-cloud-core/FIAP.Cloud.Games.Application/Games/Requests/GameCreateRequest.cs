using FIAP.Cloud.Games.Domain.Games.Rules;

namespace FIAP.Cloud.Games.Application.Games.Requests
{
    public record GameCreateRequest
    {
        public required string Name { get; init; }
        public required decimal Pricing { get; init; }
        public required GameCategoryEnum Category { get; init; }
    }
}
