using FIAP.Cloud.Games.Core.Domains;
using FIAP.Cloud.Games.Domain.Games.Rules;

namespace FIAP.Cloud.Games.Domain.Libraries.ValueObjects
{
    public class CategoryItemValueObject : ValueObject
    {
        public Guid GameId { get; set; }
        public Guid LibraryId { get; set; }
        public GameCategoryEnum Category { get; set; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return GameId;
            yield return LibraryId;
            yield return Category;
        }
    }
}
