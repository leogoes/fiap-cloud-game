using FIAP.Cloud.Games.Domain.Games.Rules;

namespace FIAP.Cloud.Games.Application.Games.Filters
{
    public class GameFilterBuilder
    {
        private readonly GameFilter Filter;

        public GameFilterBuilder()
        {
            Filter = new GameFilter();
        }

        public GameFilterBuilder WithGameId(Guid? gameId)
        {
            Filter.SetGameId(gameId);
            return this;
        }

        public GameFilterBuilder WithName(string? name)
        {
            Filter.SetName(name);
            return this;
        }

        public GameFilterBuilder WithCategory(GameCategoryEnum? category)
        {
            Filter.SetCategory(category);
            return this;
        }

        public GameFilterBuilder WithPricing(decimal? pricing)
        {
            Filter.SetPricing(pricing);
            return this;
        }

        public GameFilter Build()
        {
            return Filter;
        }
    }
}
