using Dapper;
using FIAP.Cloud.Games.Application.Games.Abstractions;
using FIAP.Cloud.Games.Application.Games.Filters;
using FIAP.Cloud.Games.Data.Contexts;
using FIAP.Cloud.Games.Domain.Games.Entities;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace FIAP.Cloud.Games.Data.Repositories
{
    public class GameRepository(GameContext context) : IGameRepository
    {
        private readonly GameContext Context = context;

        public IUnitOfWork UnitOfWork => Context;

        public async Task CommitAsync(CancellationToken cancellationToken)
        {
            await Context.SaveChangesAsync(cancellationToken);
        }

        public async Task<bool> ChangeGame(Game game)
        {
            return await Context.Games
                    .Where(x => x.Id == game.Id)
                    .ExecuteUpdateAsync(x => x
                    .SetProperty(p => p.Name, game.Name)
                    .SetProperty(p => p.Pricing, game.Pricing)
                    .SetProperty(p => p.Category, game.Category)) > 0;
        }

        public async Task CreateAsync(Game game)
        {
            await Context.Games.AddAsync(game);
        }

        public async Task<bool> DisableGameAsync(Guid gameId)
        {
            return await Context.Games
                    .Where(x => x.Id == gameId)
                    .ExecuteUpdateAsync(x => x
                    .SetProperty(p => p.IsActive, false)) > 0;
        }

        public async Task<bool> EnableGameAsync(Guid gameId)
        {
            return await Context.Games
                    .Where(x => x.Id == gameId)
                    .ExecuteUpdateAsync(x => x
                    .SetProperty(p => p.IsActive, true)) > 0;
        }

        public async Task<IEnumerable<Game>> FindAllAsync(GameFilter filter)
        {
            using IDbConnection connection = Context.GetConnection();

            var sql = "SELECT * FROM Games WHERE 1=1";
            var parameters = new DynamicParameters();

            if (filter.Name is not null && filter.Name != string.Empty)
            {
                sql += " AND NAME LIKE @Name";
                parameters.Add("Name", $"%{filter.Name}%");
            }

            if (filter.Category is not null)
            {
                sql += " AND Category = @Category";
                parameters.Add("Category", $"{filter.Category}");
            }

            if (filter.Pricing is not null && filter.Pricing > 0)
            {
                sql += " AND Pricing <= @Pricing";
                parameters.Add("Pricing", $"{filter.Pricing}");
            }

            return await connection.QueryAsync<Game>(sql, parameters);
        }

        public async Task<Game?> FindByIdAsync(Guid gameId)
        {
            return await Context.Games.FindAsync(gameId);
        }
    }
}
