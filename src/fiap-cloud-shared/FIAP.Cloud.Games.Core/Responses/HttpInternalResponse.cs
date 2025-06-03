using FIAP.Cloud.Games.Core.Exceptions;
using System.Net;

namespace FIAP.Cloud.Games.Core.Responses
{
    public class HttpInternalResponse<SuccessContent, ErrorContent> where SuccessContent : class 
                                                            where ErrorContent : class
    {
        public SuccessContent? Content { get; set; }
        public ErrorContent? InvalidContent { get; set; }
        public HttpStatusCode StatusCode { get; set; }

        public bool Error { get; set; }   
        public IEnumerable<ErrorDetail> Errors {get; set;}

        public HttpInternalResponse() 
        {
            Errors = [];
        }

        public HttpInternalResponse(bool error, IEnumerable<ErrorDetail> errors, ErrorContent content)
        {
            Error = error;
            Errors = errors;
            InvalidContent = content;
        }

        public HttpInternalResponse(bool error, IEnumerable<ErrorDetail> errors)
        {
            Error = error;
            Errors = errors;
        }

        public HttpInternalResponse(SuccessContent content)
        {
            Content = content;
            Errors = [];
        }
    }
}
