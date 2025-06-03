using FIAP.Cloud.Games.Core.Exceptions;

namespace FIAP.Cloud.Games.Core.Responses.Https
{
    public class Response409Error : ResponseErrorDetail
    {
        public Response409Error() : base(ResponseStatusCodeConst.CONFLICT.Key, ResponseStatusCodeConst.CONFLICT.Value)
        {
            
        }
    }
}
