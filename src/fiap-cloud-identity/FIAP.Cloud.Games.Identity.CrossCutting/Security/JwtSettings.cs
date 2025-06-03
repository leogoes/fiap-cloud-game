namespace FIAP.Cloud.Games.Identity.CrossCutting.Security
{
    public class JwtSettings
    {
        public required string SECRET { get; set; }
        public required string EXPIRES_IN_HOURS { get; set; }
        public required string ISSUER { get; set; }
        public required string VALID_AT { get; set; }
    }
}
