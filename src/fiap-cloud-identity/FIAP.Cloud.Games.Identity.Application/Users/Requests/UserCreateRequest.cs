namespace FIAP.Cloud.Games.Identity.Application.Users.Requests
{
    public class UserCreateRequest
    {
        public required string Name { get; set; }
        public required string Password { get; set; }
        public required string Email { get; set; }
    }
}
