using FIAP.Cloud.Games.Core.Responses;
using FIAP.Cloud.Games.Core.Exceptions;
using FIAP.Cloud.Games.Domain.Libraries.Entities;
using FIAP.Cloud.Games.Application.Libraries.Requests;
using FIAP.Cloud.Games.Application.Libraries.Responses;
using FIAP.Cloud.Games.Application.Libraries.Filters;
using FIAP.Cloud.Games.Application.Libraries.Abstractions;
using FIAP.Cloud.Games.Application.Libraries.Exceptions;
using FIAP.Cloud.Games.Domain.Games.Exceptions;
using FIAP.Cloud.Games.Application.Games.Mappers;

namespace FIAP.Cloud.Games.Application.Libraries.Services
{
    public class LibraryService(ILibraryRepository repository)
    {
        public async Task<InternalResponse<LibraryResponse>> FindAsync(LibraryFindRequest request)
        {
            if (request is { UserId: null, LibraryId: null})
                return DefaultErrorFactory.GetInvalidResource<LibraryResponse>();

            var filter = new LibraryFilterBuilder()
                    .WithLibraryId(request.LibraryId)
                    .WithUserId(request.UserId)
                    .Build();

            var library = await repository.FindAsync(filter);

            if (library is null)
                return DefaultErrorFactory.GetErrors<LibraryResponse>(LibraryErrorConst.LIBRARY_NOT_FOUND);

            return new InternalResponse<LibraryResponse>(LibraryMapper.SetLibrary(library));
        }

        public async Task<InternalResponse<LibraryResponse>> CreateAsync(LibraryCreateRequest request)
        {
            if (request is { UserId: null })
                return DefaultErrorFactory.GetInvalidResource<LibraryResponse>();

            var newLibrary = new Library(request.UserId.Value);

            var resultOfCreation = await repository.CreateAsync(newLibrary);

            if (!resultOfCreation)
                return DefaultErrorFactory.GetErrors<LibraryResponse>(LibraryErrorConst.LIBRARY_COULD_NOT_BE_CREATED);

            return new InternalResponse<LibraryResponse>(LibraryMapper.SetLibrary(newLibrary));
        }
    }
}