using Bogus;
using FIAP.Cloud.Games.Domain.Libraries.Entities;

namespace FIAP.Cloud.Games.Tests.Units.Domain.Libraries.Core.Fixtures
{
    public class LibraryDomainFixture
    {
        public static Faker<Library> GetLibrary() => new Faker<Library>()
                .RuleFor(l => l.UserId, f => f.Random.Guid())
                .RuleFor(l => l.CreatedAt, f => f.Date.Past())
                .RuleFor(l => l.UpdatedAt, f => f.Date.Past())
                .RuleFor(l => l.DeletedAt, f => f.Date.Future())
                .RuleFor(l => l.Items, f => [])
                .RuleFor(l => l.RecentPlayed, f => [])
                .RuleFor(l => l.ItemsPerCategory, f => []);
    }
}
