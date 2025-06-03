using FIAP.Cloud.Games.Core.Domains;
using FIAP.Cloud.Games.Domain.Libraries.ValueObjects;

namespace FIAP.Cloud.Games.Domain.Libraries.Entities
{
    public class Library(Guid userId) : Entity, IAggregateRoot
    {
        public Guid UserId { get; set; } = userId;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; }
        public DateTime DeletedAt { get; set; }

        public IEnumerable<LibraryItemValueObject> Items { get; set; } = [];
        public IEnumerable<LibraryItemValueObject> RecentPlayed { get; set; } = [];
        public IEnumerable<CategoryItemValueObject> ItemsPerCategory { get; set; } = [];

    }
}
