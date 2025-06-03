namespace FIAP.Cloud.Games.Identity.Application.Users.Requests
{
    public record UserAuthenticateRequest
    {
        public required string Email { get; init; }
        public required string Password { get; init; }
    }
}
