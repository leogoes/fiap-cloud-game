using FIAP.Cloud.Games.Domain.Libraries.Entities;
using FIAP.Cloud.Games.Tests.Units.Domain.Libraries.Core.Fixtures;

namespace FIAP.Cloud.Games.Tests.Units.Domain.Libraries.Entities
{
    public class LibraryTests
    {
        [Fact]
        public void Constructor_ShouldSetUserIdAndCreatedAt()
        {
            var userId = Guid.NewGuid();

            var library = new Library(userId);

            Assert.Equal(userId, library.UserId);
            Assert.NotEqual(default(DateTime), library.CreatedAt);
            Assert.NotEqual(default(DateTime), library.UpdatedAt);
            Assert.Equal(default(DateTime), library.DeletedAt);
            Assert.NotNull(library.Items);
            Assert.Empty(library.Items);
            Assert.NotNull(library.RecentPlayed);
            Assert.Empty(library.RecentPlayed);
            Assert.NotNull(library.ItemsPerCategory);
            Assert.Empty(library.ItemsPerCategory);
        }

        [Fact]
        public void UserIdProperty_ShouldBeSettable()
        {
            var library = LibraryDomainFixture.GetLibrary().Generate();
            var newUserId = Guid.NewGuid();

            library.UserId = newUserId;

            Assert.Equal(newUserId, library.UserId);
        }

        [Fact]
        public void CreatedAtProperty_ShouldBeInitializedOnCreation()
        {
            var userId = Guid.NewGuid();

            var library = new Library(userId);

            Assert.NotEqual(default(DateTime), library.CreatedAt);
        }

        [Fact]
        public void UpdatedAtProperty_ShouldBeInitializedOnCreation()
        {
            var userId = Guid.NewGuid();

            var library = new Library(userId);

            Assert.NotEqual(default(DateTime), library.UpdatedAt);
            Assert.Equal(library.CreatedAt, library.UpdatedAt);
        }

        [Fact]
        public void DeletedAtProperty_ShouldBeDefaultDateTimeOnCreation()
        {
            var userId = Guid.NewGuid();

            var library = new Library(userId);

            Assert.Equal(default(DateTime), library.DeletedAt);
        }

        [Fact]
        public void ItemsProperty_ShouldBeInitializedAsEmptyList()
        {
            var userId = Guid.NewGuid();

            var library = new Library(userId);

            Assert.NotNull(library.Items);
            Assert.Empty(library.Items);
        }

        [Fact]
        public void RecentPlayedProperty_ShouldBeInitializedAsEmptyList()
        {
            var userId = Guid.NewGuid();

            var library = new Library(userId);

            Assert.NotNull(library.RecentPlayed);
            Assert.Empty(library.RecentPlayed);
        }

        [Fact]
        public void ItemsPerCategoryProperty_ShouldBeInitializedAsEmptyList()
        {
            var userId = Guid.NewGuid();

            var library = new Library(userId);

            Assert.NotNull(library.ItemsPerCategory);
            Assert.Empty(library.ItemsPerCategory);
        }

        [Fact]
        public void CanGenerateLibraryObjectWithBogus()
        {
            var library = LibraryDomainFixture.GetLibrary().Generate();

            Assert.NotNull(library);
            Assert.NotEqual(Guid.Empty, library.UserId);
            Assert.NotEqual(default(DateTime), library.CreatedAt);
            Assert.NotEqual(default(DateTime), library.UpdatedAt);
            Assert.NotEqual(default(DateTime), library.DeletedAt);
            Assert.NotNull(library.Items);
            Assert.NotNull(library.RecentPlayed);
            Assert.NotNull(library.ItemsPerCategory);
        }

        [Fact]
        public void CanGenerateMultipleLibraryObjectsWithBogus()
        {
            var libraries = LibraryDomainFixture.GetLibrary().Generate(5);

            Assert.NotNull(libraries);
            Assert.Equal(5, libraries.Count());
            foreach (var library in libraries)
            {
                Assert.NotEqual(Guid.Empty, library.UserId);
                Assert.NotNull(library.Items);
                Assert.NotNull(library.RecentPlayed);
                Assert.NotNull(library.ItemsPerCategory);
            }
        }
    }
}
