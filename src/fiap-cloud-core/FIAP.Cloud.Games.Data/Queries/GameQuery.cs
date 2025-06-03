using FIAP.Cloud.Games.Data.Contexts;
using FIAP.Cloud.Games.Domain.Games.Entities;
using Microsoft.EntityFrameworkCore;

namespace FIAP.Cloud.Games.Data.Queries
{
    public class GameQuery
    {
        [UseProjection]
        [UseFiltering]
        public IQueryable<Game> GetGames([Service] GameContext context) => context.Games
            .AsNoTracking()
            .OrderBy(x => x.Name);
    }
}
