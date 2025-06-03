using FIAP.Cloud.Games.Core.Responses;

namespace FIAP.Cloud.Games.Core.Exceptions
{
    public class DefaultErrorFactory
    {
        public static InternalResponse<T> GetNotFound<T>() where T : class
        {
            return new InternalResponse<T>()
            {
                Slug = DefaultErrorConst.NOT_FOUND.Key,
                Message = DefaultErrorConst.NOT_FOUND.Value,
                Error = true,
                Conflict = false,
                Content = null,
                NotFound = true
            };
        }

        public static InternalResponse<T> GetConflict<T>() where T : class
        {
            return new InternalResponse<T>()
            {
                Slug = DefaultErrorConst.CONFLICT_RESOURCE.Key,
                Message = DefaultErrorConst.CONFLICT_RESOURCE.Value,
                Error = true,
                Conflict = true,
                Content = null,
                NotFound = false
            };
        }

        public static InternalResponse<T> GetInvalidResource<T>() where T : class
        {
            return new InternalResponse<T>()
            {
                Slug = DefaultErrorConst.INVALID_RESOURCE.Key,
                Message = DefaultErrorConst.INVALID_RESOURCE.Value,
                Error = true,
                Conflict = false,
                Content = null,
                NotFound = false
            };
        }

        public static InternalResponse<T> GetErrors<T>(KeyValuePair<string, string> error, IEnumerable<ErrorDetail> errors) where T : class
        {
            return new InternalResponse<T>()
            {
                Slug = error.Key,
                Message = error.Value,
                Error = true,
                Conflict = false,
                Content = null,
                NotFound = false,
                Details = errors
            };
        }

        public static InternalResponse<T> GetErrors<T>(KeyValuePair<string, string> error) where T : class
        {
            return new InternalResponse<T>()
            {
                Slug = error.Key,
                Message = error.Value,
                Error = true,
                Conflict = false,
                Content = null,
                NotFound = false,
                Details = []
            };
        }

        public static InternalResponse<T> GetPersistenceError<T>() where T : class
        {
            return new InternalResponse<T>()
            {
                Slug = DefaultErrorConst.PERSISTENCE_ERROR.Key,
                Message = DefaultErrorConst.PERSISTENCE_ERROR.Value,
                Error = true,
                Conflict = false,
                Content = null,
                NotFound = false
            };
        }
    }
}
