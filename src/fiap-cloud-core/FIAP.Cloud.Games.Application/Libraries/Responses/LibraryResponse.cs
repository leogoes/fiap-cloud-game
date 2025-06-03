using FIAP.Cloud.Games.Domain.Libraries.ValueObjects;

namespace FIAP.Cloud.Games.Application.Libraries.Responses
{
    public class LibraryResponse
    {
        public Guid UserId { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime DeletedAt { get; set; }

        public IEnumerable<LibraryItemValueObject> Items { get; set; } = [];
        public IEnumerable<LibraryItemValueObject> RecentPlayed { get; set; } = [];
        public IEnumerable<CategoryItemValueObject> ItemsPerCategory { get; set; } = [];
    }
}