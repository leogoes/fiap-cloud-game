namespace FIAP.Cloud.Games.Core.Responses.Https
{
    public class Response422Error()
        : ResponseErrorDetail(ResponseStatusCodeConst.UNPROCESSABLE_CONTENT.Key, ResponseStatusCodeConst.UNPROCESSABLE_CONTENT.Value)
    {

    }

    public class Response422Error<T>(InternalResponse<T> internalResponse) 
        : ResponseErrorDetail(internalResponse.Slug, internalResponse.Message, internalResponse.Details) where T : class
    {
    }
}
