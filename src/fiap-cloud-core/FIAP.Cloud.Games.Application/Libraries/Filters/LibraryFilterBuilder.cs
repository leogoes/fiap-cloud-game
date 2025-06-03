namespace FIAP.Cloud.Games.Application.Libraries.Filters
{
    public class LibraryFilterBuilder
    {
        private LibraryFilter Filter;

        public LibraryFilterBuilder()
        {
            Filter = new LibraryFilter();
        }

        public LibraryFilterBuilder WithLibraryId(Guid? libraryId)
        {
            Filter.SetLibraryId(libraryId);
            return this;
        }

        public LibraryFilterBuilder WithUserId(Guid? userId)
        {
            Filter.SetUserId(userId);
            return this;
        }

        public LibraryFilter Build()
        {
            return Filter;
        }
    }
}
