using FIAP.Cloud.Games.Application.Games.Responses.Core;
using FIAP.Cloud.Games.Domain.Games.Entities;

namespace FIAP.Cloud.Games.Application.Games.Mappers
{
    public class GameMapper
    {
        public static GameResponse SetGame(Game game)
        {
            return new GameResponse
            {
                GameId = game.Id,
                Category = game.Category,
                CreatedAt = game.CreatedAt,
                Name = game.Name,
                Pricing = game.Pricing,
                UpdatedAt = game.UpdatedAt
            };
        }

        public static IEnumerable<GameResponse> SetGame(IEnumerable<Game> games)
        {
            return games.Select(SetGame);
        }
    }
}
