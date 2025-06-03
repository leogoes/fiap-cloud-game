using FIAP.Cloud.Games.Domain.Games.Entities;
using FIAP.Cloud.Games.Domain.Games.Exceptions;
using FIAP.Cloud.Games.Domain.Games.Rules;
using FIAP.Cloud.Games.Tests.Units.Domain.Games.Core.Fixtures;

namespace FIAP.Cloud.Games.Tests.Units.Domain.Games.Entities
{
    public class GameTests
    {
        [Fact]
        public void Constructor_ShouldSetPropertiesCorrectly()
        {
            var name = "Jogo Teste";
            var pricing = 29.99m;
            var category = GameCategoryEnum.Action;

            var game = new Game(name, pricing, category);

            Assert.Equal(name, game.Name);
            Assert.Equal(pricing, game.Pricing);
            Assert.Equal(category, game.Category);
            Assert.True(game.IsActive);
            Assert.NotEqual(default, game.CreatedAt);
            Assert.NotEqual(default, game.UpdatedAt);
            Assert.Equal(default, game.DeletedAt);
            Assert.Empty(game.Errors);
        }

        [Fact]
        public void ChangePrice_WithValidPrice_ShouldUpdatePricingAndUpdatedAt()
        {
            var game = GameDomainFixture.GetGame().Generate();
            var newPrice = 49.99m;
            var originalUpdatedAt = game.UpdatedAt;

            game.ChangePrice(newPrice);

            Assert.Equal(newPrice, game.Pricing);
            Assert.NotEqual(originalUpdatedAt, game.UpdatedAt);
            Assert.Empty(game.Errors);
        }

        [Fact]
        public void ChangePrice_WithInvalidPrice_ShouldAddError()
        {
            var game = GameDomainFixture.GetGame().Generate();
            var invalidPrice = -5.00m;

            game.ChangePrice(invalidPrice);

            Assert.Equal(invalidPrice, game.Pricing);
            Assert.NotEmpty(game.Errors);
            Assert.Contains(GameErrorFactory.GetGamePricingMustBeGreaterThanZero(), game.Errors);
        }

        [Fact]
        public void ChangeName_WithValidName_ShouldUpdateNameAndUpdatedAt()
        {
            var game = GameDomainFixture.GetGame().Generate();
            var newName = "Novo Jogo Teste";
            var originalUpdatedAt = game.UpdatedAt;

            game.ChangeName(newName);

            Assert.Equal(newName, game.Name);
            Assert.NotEqual(originalUpdatedAt, game.UpdatedAt);
            Assert.Empty(game.Errors);
        }

        [Theory]
        [InlineData("Jo")]
        [InlineData("Nome de jogo muitoooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooo longo")]
        public void ChangeName_WithInvalidName_ShouldAddError(string invalidName)
        {
            var game = GameDomainFixture.GetGame().Generate();

            game.ChangeName(invalidName);

            Assert.Equal(invalidName, game.Name);
            Assert.NotEmpty(game.Errors);
            Assert.Contains(GameErrorFactory.GetGameNameNotValid(), game.Errors);
        }

        [Fact]
        public void ChangeCategory_WithNewCategory_ShouldUpdateCategoryAndUpdatedAt()
        {
            var game = GameDomainFixture.GetGame().RuleFor(f => f.Category, GameCategoryEnum.Action).Generate();
            var newCategory = GameCategoryEnum.Horror;

            game.ChangeCategory(newCategory);

            Assert.Equal(newCategory, game.Category);
            Assert.NotEqual(game.CreatedAt, game.UpdatedAt);
            Assert.Empty(game.Errors);
        }

        [Fact]
        public void ChangeCategory_WithSameCategory_ShouldAddError()
        {
            var game = GameDomainFixture.GetGame().RuleFor(f => f.Category, GameCategoryEnum.Action).Generate();
            var sameCategory = GameCategoryEnum.Action;

            game.ChangeCategory(sameCategory);

            Assert.Equal(sameCategory, game.Category);
            Assert.NotEmpty(game.Errors);
            Assert.Contains(GameErrorFactory.GetGameAlreadyInCategory(), game.Errors);
        }

        [Fact]
        public void GameIsInCategory_WithSameCategory_ShouldReturnTrue()
        {
            var game = GameDomainFixture.GetGame().RuleFor(f => f.Category, GameCategoryEnum.Action).Generate();
            var categoryToCheck = GameCategoryEnum.Action;

            var result = game.GameIsInCategory(categoryToCheck);

            Assert.True(result);
        }

        [Fact]
        public void GameIsInCategory_WithDifferentCategory_ShouldReturnFalse()
        {
            var game = GameDomainFixture.GetGame().RuleFor(f => f.Category, GameCategoryEnum.Action).Generate();
            var differentCategory = GameCategoryEnum.Horror;

            var result = game.GameIsInCategory(differentCategory);

            Assert.False(result);
        }
    }
}
