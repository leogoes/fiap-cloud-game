using FIAP.Cloud.Games.Application.Games.Responses.Core;
using FIAP.Cloud.Games.Application.Libraries.Responses;
using FIAP.Cloud.Games.Domain.Games.Entities;
using FIAP.Cloud.Games.Domain.Libraries.Entities;

namespace FIAP.Cloud.Games.Application.Games.Mappers
{
    public class LibraryMapper
    {
        public static LibraryResponse SetLibrary(Library library)
        {
            return new LibraryResponse
            {
                CreatedAt = library.CreatedAt,
                DeletedAt = library.DeletedAt,
                Items = library.Items,
                ItemsPerCategory = library.ItemsPerCategory,
                RecentPlayed = library.RecentPlayed,
                UpdatedAt = library.UpdatedAt,
                UserId = library.UserId 
            };
        }

        public static IEnumerable<LibraryResponse> SetLibraries(IEnumerable<Library> libraries)
        {
            return libraries.Select(SetLibrary);
        }
    }
}
