using FIAP.Cloud.Games.Application.Libraries.Filters;
using FIAP.Cloud.Games.Domain.Libraries.Entities;

namespace FIAP.Cloud.Games.Application.Libraries.Abstractions
{
    public interface ILibraryRepository
    {
        public Task<Library?> FindAsync(LibraryFilter filter);
        public Task<Library?> FindByIdAsync(Guid libraryId);
        public Task<Library?> FindByUserAsync(Guid userId);
        public Task<bool> CreateAsync(Library library);
    }
}
