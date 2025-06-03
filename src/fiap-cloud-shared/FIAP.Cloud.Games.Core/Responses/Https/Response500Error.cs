namespace FIAP.Cloud.Games.Core.Responses.Https
{
    public class Response500Error()
        : ResponseErrorDetail(ResponseStatusCodeConst.INTERNAL_SERVER_ERROR.Key, ResponseStatusCodeConst.INTERNAL_SERVER_ERROR.Value)
    {

    }
}
