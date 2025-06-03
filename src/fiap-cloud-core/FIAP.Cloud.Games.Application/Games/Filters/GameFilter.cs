using FIAP.Cloud.Games.Domain.Games.Rules;

namespace FIAP.Cloud.Games.Application.Games.Filters
{
    public class GameFilter
    {
        public Guid? GameId { get; set; }
        public string? Name { get; set; }
        public decimal? Pricing { get; set; }
        public GameCategoryEnum? Category { get; set; }

        public void SetGameId(Guid? gameId)
        {
            if (gameId is null || gameId == Guid.Empty)
                return;

            GameId = gameId;
        }

        public void SetName(string? name)
        {
            if (string.IsNullOrEmpty(name))
                return;

            Name = name;
        }

        public void SetCategory(GameCategoryEnum? category)
        {
            if( category is null || !Enum.IsDefined(typeof(GameCategoryEnum), category))
                return;

            Category = category;
        }

        public void SetPricing(decimal? pricing)
        {
            if( pricing is null || pricing is 0)
                return;

            Pricing = pricing;
        }
    }
}
