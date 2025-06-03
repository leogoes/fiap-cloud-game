using Bogus;
using FIAP.Cloud.Games.Domain.Games.Entities;
using FIAP.Cloud.Games.Domain.Games.Rules;

namespace FIAP.Cloud.Games.Tests.Units.Domain.Games.Core.Fixtures
{
    public class GameDomainFixture
    {
        public static Faker<Game> GetGame() =>
            new Faker<Game>()
            .RuleFor(g => g.Name, f => f.Lorem.Word())
            .RuleFor(g => g.Pricing, f => f.Random.Decimal(1, 100))
            .RuleFor(g => g.Category, f => f.PickRandom<GameCategoryEnum>())
            .RuleFor(g => g.CreatedAt, f => f.Date.Past())
            .RuleFor(g => g.UpdatedAt, f => f.Date.Past())
            .RuleFor(g => g.DeletedAt, f => f.Date.Future())
            .RuleFor(g => g.IsActive, f => f.Random.Bool());
    }
}
