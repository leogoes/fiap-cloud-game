using FIAP.Cloud.Games.SDK.Games.Services;
using FIAP.Cloud.Games.SDK.Libraries.Services;

namespace FIAP.Cloud.Games.SDK
{
    public class CloudGamesClient(GameClient gameClient, LibraryClient libraryClient)
    {
        public GameClient GameClient { get; } = gameClient;
        public LibraryClient LibraryClient { get; } = libraryClient;
    }
}
