using FIAP.Cloud.Games.Application.Games.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace FIAP.Cloud.Games.Data.Contexts
{
    public class LibraryContext(IMongoClient client, IServiceProvider provider) : IUnitOfWorkNoSql
    {
        static readonly ClientSessionOptions _sessionOptions = new()
        {
            DefaultTransactionOptions = new TransactionOptions(ReadConcern.Majority, ReadPreference.Primary, WriteConcern.WMajority)
        };

        protected readonly IMongoClient _client = client;
        protected readonly object _lock = new();
        protected readonly IServiceProvider _provider = provider;
        protected Task<IClientSessionHandle>? _session;

        protected readonly List<Func<Task>> Commands = [];

        public IClientSessionHandle? Session { get; private set; }
        public Guid? TransactionId { get; private set; }

        public void Dispose()
        {
            Session?.Dispose();
        }

        public Task<IClientSessionHandle> StartSession(CancellationToken cancellationToken)
        {
            async Task<IClientSessionHandle> Start()
            {
                var handle = await _client.StartSessionAsync(_sessionOptions, cancellationToken);

                Session = handle;

                return handle;
            }

            lock (_lock)
            {
                if (_session != null)
                    return _session;

                _session = Start();

                return _session;
            }
        }

        public async Task BeginTransaction(CancellationToken cancellationToken)
        {
            var session = await StartSession(cancellationToken);

            if (!session.IsInTransaction)
            {
                session.StartTransaction();
                TransactionId = Guid.NewGuid();
            }
        }

        public async Task CommitTransaction(CancellationToken cancellationToken)
        {
            if (Session == null)
                throw new InvalidOperationException("No session has been created");

            if (Session.IsInTransaction == false)
                throw new InvalidOperationException("The session is not in an active transaction");

            await Session.CommitTransactionAsync(cancellationToken);
            TransactionId = null;
        }

        public async Task AbortTransaction(CancellationToken cancellationToken)
        {
            if (Session == null)
                throw new InvalidOperationException("No session has been created");

            if (Session.IsInTransaction == false)
                throw new InvalidOperationException("The session is not in an active transaction");

            await Session.AbortTransactionAsync(cancellationToken);
            TransactionId = null;
        }

        public IMongoCollection<T> GetCollection<T>()
        {
            return _provider.GetRequiredService<IMongoCollection<T>>();
        }

        public async Task<bool> CommitAsync(CancellationToken cancellationToken)
        {
            await BeginTransaction(cancellationToken);

            try
            {
                await CommitTransaction(cancellationToken);
            }
            catch (MongoCommandException)
            {
                await AbortTransaction(cancellationToken);
                throw;
            }
            catch (Exception)
            {
                await AbortTransaction(cancellationToken);
                throw;
            }

            return Commands.Count > 0;
        }

        public void AddCommand(Func<Task> command)
        {
            Commands.Add(command);
        }
    }
}
