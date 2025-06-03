using FIAP.Cloud.Games.Domain.Games.Rules;

namespace FIAP.Cloud.Games.Application.Games.Responses.Core
{
    public class GameResponse
    {
        public Guid GameId { get; set; }
        public string Name { get; set; }
        public decimal Pricing { get; set; }
        public GameCategoryEnum Category { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
