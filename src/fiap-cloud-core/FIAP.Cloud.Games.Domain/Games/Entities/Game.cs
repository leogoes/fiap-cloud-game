using FIAP.Cloud.Games.Core.Domains;
using FIAP.Cloud.Games.Domain.Games.Exceptions;
using FIAP.Cloud.Games.Domain.Games.Rules;

namespace FIAP.Cloud.Games.Domain.Games.Entities
{
    public class Game : Entity, IAggregateRoot
    {
        public string Name { get; set; }
        public decimal Pricing { get; set; }

        public GameCategoryEnum Category { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime DeletedAt { get; set; }

        public bool IsActive { get; set; }

        public Game(string name, decimal pricing, GameCategoryEnum category)
        {
            Name = name;
            Pricing = pricing;
            Category = category;
            IsActive = true;
        }

        public Game() { }

        public void ChangePrice(decimal newPrice)
        {
            if (newPrice < 0)
                AddError(GameErrorFactory.GetGamePricingMustBeGreaterThanZero());

            Pricing = newPrice;
            UpdatedAt = DateTime.UtcNow;
        }

        public void ChangeName(string name)
        {
            if (name.Length < 3 || name.Length > 100)
                AddError(GameErrorFactory.GetGameNameNotValid());

            Name = name;
            UpdatedAt = DateTime.UtcNow;
        }

        public void ChangeCategory(GameCategoryEnum category)
        {
            if(GameIsInCategory(category))
                AddError(GameErrorFactory.GetGameAlreadyInCategory());

            Category = category;
            UpdatedAt = DateTime.UtcNow;
        }

        public bool GameIsInCategory(GameCategoryEnum category)
        {
            return Category == category;
        }
    }
}
