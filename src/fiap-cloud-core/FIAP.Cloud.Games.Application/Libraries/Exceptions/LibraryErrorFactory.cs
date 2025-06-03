using FIAP.Cloud.Games.Core.Exceptions;
using FIAP.Cloud.Games.Domain.Libraries.Entities;

namespace FIAP.Cloud.Games.Application.Libraries.Exceptions
{
    public static class LibraryErrorFactory
    {
        public static ErrorDetail GetLibraryNotFound()
        {
            return new ErrorDetail
            {
                Slug = LibraryErrorConst.LIBRARY_NOT_FOUND.Key,
                Message = LibraryErrorConst.LIBRARY_NOT_FOUND.Value,
                Location = nameof(Library),
                Field = "Library"
            };
        }
    }
}
