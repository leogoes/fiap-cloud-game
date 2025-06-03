using FIAP.Cloud.Games.Core.Domains;

namespace FIAP.Cloud.Games.Domain.Libraries.ValueObjects
{
    public class LibraryItemValueObject : ValueObject
    {
        public Guid GameId { get; set; }
        public Guid LibraryId { get; set; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return GameId;
            yield return LibraryId;
        }
    }
}
