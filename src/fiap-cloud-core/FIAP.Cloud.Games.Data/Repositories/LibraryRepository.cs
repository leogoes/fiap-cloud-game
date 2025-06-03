using FIAP.Cloud.Games.Application.Games.Abstractions;
using FIAP.Cloud.Games.Application.Libraries.Abstractions;
using FIAP.Cloud.Games.Application.Libraries.Filters;
using FIAP.Cloud.Games.Data.Contexts;
using FIAP.Cloud.Games.Domain.Libraries.Entities; 
using MongoDB.Driver;

namespace FIAP.Cloud.Games.Data.Repositories
{
    public class LibraryRepository(LibraryContext context) : ILibraryRepository
    {
        public readonly IUnitOfWorkNoSql UnitOfWork = context;
        public readonly LibraryContext Context = context;

        private readonly IMongoCollection<Library> _aggregateRootCollection = context.GetCollection<Library>();


        public void Create(Library library)
        {
            Context.AddCommand(() => CreateAsync(library));
        }

        public async Task<bool> CreateAsync(Library library)
        {
            try
            {
                await _aggregateRootCollection.InsertOneAsync(library);
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public async Task<Library?> FindAsync(LibraryFilter filter)
        {
            var builder = Builders<Library>.Filter;
            var filterDefinition = builder.Empty;

            if(filter.LibraryId is not null)
                filterDefinition &= builder.Eq(x => x.Id, filter.LibraryId);

            if (filter.UserId is not null)
                filterDefinition &= builder.Eq(x => x.UserId, filter.UserId);

            return await _aggregateRootCollection.Find(filterDefinition).FirstOrDefaultAsync();
        }

        public async Task<Library?> FindByIdAsync(Guid libraryId)
        {
            return await _aggregateRootCollection.Find(x => x.Id == libraryId).FirstOrDefaultAsync();
        }

        public async Task<Library?> FindByUserAsync(Guid userId)
        {
            return await _aggregateRootCollection.Find(x => x.UserId == userId).FirstOrDefaultAsync();
        }
    }
}
