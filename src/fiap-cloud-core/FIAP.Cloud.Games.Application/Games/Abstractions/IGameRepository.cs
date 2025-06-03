using FIAP.Cloud.Games.Application.Games.Filters;
using FIAP.Cloud.Games.Domain.Games.Entities;

namespace FIAP.Cloud.Games.Application.Games.Abstractions
{
    public interface IGameRepository
    {
        public IUnitOfWork UnitOfWork { get; }

        public Task<Game?> FindByIdAsync(Guid gameId);
        public Task<IEnumerable<Game>> FindAllAsync(GameFilter filter);
        public Task CreateAsync(Game game);
        public Task<bool> ChangeGame(Game game);
        public Task<bool> DisableGameAsync(Guid gameId);
        public Task<bool> EnableGameAsync(Guid gameId);
    }

    public interface IUnitOfWork
    {
        public Task<bool> CommitAsync(CancellationToken cancellationToken);
    }

    public interface IUnitOfWorkNoSql
    {
        public Task<bool> CommitAsync(CancellationToken cancellationToken);
        void AddCommand(Func<Task> command);
    }
}
