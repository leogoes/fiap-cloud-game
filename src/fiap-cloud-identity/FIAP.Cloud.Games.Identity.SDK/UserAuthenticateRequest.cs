namespace FIAP.Cloud.Games.Identity.SDK
{
    public record UserAuthenticateRequest
    {
        public required string Email { get; init; }
        public required string Password { get; init; }
    }
}
