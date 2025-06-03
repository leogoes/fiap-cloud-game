namespace FIAP.Cloud.Games.Application.Libraries.Requests
{
    public class LibraryFindRequest
    {
        public LibraryFindRequest(Guid? userId, Guid? libraryId)
        {
            UserId = userId;
            LibraryId = libraryId;
        }

        public Guid? UserId { get; set; }
        public Guid? LibraryId { get; set; }
    }
}