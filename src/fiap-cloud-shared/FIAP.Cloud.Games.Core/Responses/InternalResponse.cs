using FIAP.Cloud.Games.Core.Exceptions;

namespace FIAP.Cloud.Games.Core.Responses
{
    public class InternalResponse<T> where T : class
    {
        public T? Content { get; set; }
        public string Slug { get; set; }
        public string Message { get; set; }

        public bool Error { get; set; }   
        public bool NotFound { get; set; }   
        public bool Conflict { get; set; }

        public IEnumerable<ErrorDetail> Details {get; set;}

        public InternalResponse() 
        {
            Details = [];
        }

        public InternalResponse(bool error, IEnumerable<ErrorDetail> errors)
        {
            Error = error;
            Details = errors;
        }

        public InternalResponse(T content)
        {
            Content = content;
            Details = [];
        }

        public bool CheckBySlug(KeyValuePair<string, string> keyValue, string slug)
        {
            return keyValue.Key.Equals(slug);
        }

        public bool CheckBySlug(string keyValue, string slug)
        {
            return keyValue.Equals(slug);
        }
    }
}
