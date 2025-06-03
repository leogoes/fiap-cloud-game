using FIAP.Cloud.Games.Domain.Games.Rules;

namespace FIAP.Cloud.Games.Application.Games.Responses
{
    public record GameCreateResponse(string Name, decimal Pricing, GameCategoryEnum Category, DateTime CreatedAt)
    {
        public string Name { get; set; } = Name;
        public decimal Pricing { get; set; } = Pricing;
        public GameCategoryEnum Category { get; set; } = Category;
        public DateTime CreatedAt { get; set; } = CreatedAt;

    }
}
