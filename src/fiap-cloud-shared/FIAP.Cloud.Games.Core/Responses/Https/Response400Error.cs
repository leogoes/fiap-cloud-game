using FIAP.Cloud.Games.Core.Exceptions;

namespace FIAP.Cloud.Games.Core.Responses.Https
{
    public class Response400Error : ResponseErrorDetail
    {
        public Response400Error() : base(ResponseStatusCodeConst.BAD_REQUEST.Key, ResponseStatusCodeConst.BAD_REQUEST.Value)
        {
            
        }

        public Response400Error(IEnumerable<ErrorDetail> errors) : base(ResponseStatusCodeConst.BAD_REQUEST.Key, ResponseStatusCodeConst.BAD_REQUEST.Value, errors)
        {

        }
    }

    public class Response400Error<T>(InternalResponse<T> internalResponse) : ResponseErrorDetail(internalResponse.Slug, internalResponse.Message, internalResponse.Details) where T : class
    {
    }
}
