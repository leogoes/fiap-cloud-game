using FIAP.Cloud.Games.Core.Exceptions;
using FIAP.Cloud.Games.Domain.Games.Entities;

namespace FIAP.Cloud.Games.Domain.Games.Exceptions
{
    public static class GameErrorFactory
    {
        public static ErrorDetail GetGameNameNotValid()
        {
            return new ErrorDetail
            {
                Slug = GameErrorConst.GAME_NAME_NOT_VALID.Key,
                Message = GameErrorConst.GAME_NAME_NOT_VALID.Value,
                Location = nameof(Game),
                Field = "Game.Name"
            };
        }

        public static ErrorDetail GetGamePricingMustBeGreaterThanZero()
        {
            return new ErrorDetail
            {
                Slug = GameErrorConst.GAME_PRICING_MUST_BE_GREATER_THAN_ZERO.Key,
                Message = GameErrorConst.GAME_PRICING_MUST_BE_GREATER_THAN_ZERO.Value,
                Location = nameof(Game),
                Field = "Game.Pricing"
            };
        }

        public static ErrorDetail GetGameIsNotActive()
        {
            return new ErrorDetail
            {
                Slug = GameErrorConst.GAME_IS_NOT_ACTIVE.Key,
                Message = GameErrorConst.GAME_IS_NOT_ACTIVE.Value,
                Location = nameof(Game),
                Field = "Game.IsActive"
            };
        }

        public static ErrorDetail GetGameNotFound()
        {
            return new ErrorDetail
            {
                Slug = GameErrorConst.GAME_NOT_FOUND.Key,
                Message = GameErrorConst.GAME_NOT_FOUND.Value,
                Location = nameof(Game),
                Field = "Game.Id"
            };
        }

        public static ErrorDetail GetGameAlreadyInCategory()
        {
            return new ErrorDetail
            {
                Slug = GameErrorConst.GAME_ALREADY_IN_CATEGORY.Key,
                Message = GameErrorConst.GAME_ALREADY_IN_CATEGORY.Value,
                Location = nameof(Game),
                Field = "Game.Category"
            };
        }

        public static ErrorDetail GetGameCouldNotBeCreated()
        {
            return new ErrorDetail
            {
                Slug = GameErrorConst.GAME_COULD_NOT_BE_CREATED.Key,
                Message = GameErrorConst.GAME_COULD_NOT_BE_CREATED.Value,
                Location = nameof(Game),
                Field = "Game"
            };
        }

        public static ErrorDetail GetGameCouldNotBeChanged()
        {
            return new ErrorDetail
            {
                Slug = GameErrorConst.GAME_COULD_NOT_BE_CHANGED.Key,
                Message = GameErrorConst.GAME_COULD_NOT_BE_CHANGED.Value,
                Location = nameof(Game),
                Field = "Game"
            };
        }

        public static ErrorDetail GetGameAlreadyEnable()
        {
            return new ErrorDetail
            {
                Slug = GameErrorConst.GAME_ALREADY_ENABLE.Key,
                Message = GameErrorConst.GAME_ALREADY_ENABLE.Value,
                Location = nameof(Game),
                Field = "Game.IsActive"
            };
        }

        public static ErrorDetail GetGameAlreadyDisable()
        {
            return new ErrorDetail
            {
                Slug = GameErrorConst.GAME_ALREADY_DISABLE.Key,
                Message = GameErrorConst.GAME_ALREADY_DISABLE.Value,
                Location = nameof(Game),
                Field = "Game.IsActive"
            };
        }
    }
}
