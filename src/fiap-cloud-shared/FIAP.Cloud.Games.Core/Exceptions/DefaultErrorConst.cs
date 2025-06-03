namespace FIAP.Cloud.Games.Core.Exceptions
{
    public class DefaultErrorConst
    {
        public static readonly KeyValuePair<string, string> INVALID_RESOURCE = new("INVALID_RESOURCE", "The current resources is invalid.");
        public static readonly KeyValuePair<string, string> CONFLICT_RESOURCE = new("CONFLICT_RESOURCE", "Already exists an resource with this identification.");
        public static readonly KeyValuePair<string, string> PERSISTENCE_ERROR = new("PERSISTENCE_ERROR", "An error has occurred while persist data.");
        public static readonly KeyValuePair<string, string> NOT_FOUND = new("NOT_FOUND", "Resource not found.");
    }
}
