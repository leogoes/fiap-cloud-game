namespace FIAP.Cloud.Games.Identity.Application.Users.Events
{
    public class UserCreatedEvent(string userId)
    {
        public string UserId { get; set; } = userId;
    }
}
