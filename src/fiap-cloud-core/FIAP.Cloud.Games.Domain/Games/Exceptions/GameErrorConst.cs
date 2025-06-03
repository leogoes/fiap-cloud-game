namespace FIAP.Cloud.Games.Domain.Games.Exceptions
{
    public class GameErrorConst
    {
        public static readonly KeyValuePair<string, string> GAME_COULD_NOT_BE_CREATED = new("GAME_COULD_NOT_BE_CREATED", "Game could not be created.");
        public static readonly KeyValuePair<string, string> GAME_COULD_NOT_BE_CHANGED = new("GAME_COULD_NOT_BE_CHANGED", "Game could not be changed.");

        public static readonly KeyValuePair<string, string> GAME_ALREADY_ENABLE = new("GAME_ALREADY_ENABLE", "Game is already enable.");
        public static readonly KeyValuePair<string, string> GAME_ALREADY_DISABLE = new("GAME_ALREADY_DISABLE", "Game is already disable.");
        public static readonly KeyValuePair<string, string> GAME_IS_NOT_ACTIVE = new("GAME_IS_NOT_ACTIVE", "Game is not active.");
        public static readonly KeyValuePair<string, string> GAME_NOT_FOUND = new("GAME_NOT_FOUND", "Game for the current id was not found.");
        public static readonly KeyValuePair<string, string> GAME_ALREADY_IN_CATEGORY = new("GAME_ALREADY_IN_CATEGORY", "Game is already in the requested category.");
        public static KeyValuePair<string, string> GAME_NAME_NOT_VALID = new("GAME_NAME_NOT_VALID", "Game name must be between 3 or 100 chars.");
        public static KeyValuePair<string, string> GAME_PRICING_MUST_BE_GREATER_THAN_ZERO = new("GAME_PRICING_MUST_BE_GREATER_THAN_ZERO", "Game price must have have a value.");
    }
}
