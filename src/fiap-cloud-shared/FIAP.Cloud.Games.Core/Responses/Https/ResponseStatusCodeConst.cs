namespace FIAP.Cloud.Games.Core.Responses.Https
{
    public class ResponseStatusCodeConst
    {
        public static readonly KeyValuePair<string, string> BAD_REQUEST = new("BAD_REQUEST", "Malformed request syntax.");
        public static readonly KeyValuePair<string, string> UNAUTHORIZED = new("UNAUTHORIZED", "Unauthorized.");
        public static readonly KeyValuePair<string, string> FORBIDDEN = new("FORBIDDEN", "Forbidden.");
        public static readonly KeyValuePair<string, string> CONFLICT = new("CONFLICT", "Already exists an resource with this identification.");
        public static readonly KeyValuePair<string, string> NOT_FOUND = new("NOT_FOUND", "Resource not found.");
        public static readonly KeyValuePair<string, string> UNPROCESSABLE_CONTENT = new ("UNPROCESSABLE_CONTENT", "Unable to process the request because it contains invalid data.");
        public static readonly KeyValuePair<string, string> INTERNAL_SERVER_ERROR = new ("INTERNAL_SERVER_ERROR", "Internal server error.");
    }
}
