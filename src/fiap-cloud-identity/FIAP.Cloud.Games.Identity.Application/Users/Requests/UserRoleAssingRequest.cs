using FIAP.Cloud.Games.Identity.Application.Users.Rules;

namespace FIAP.Cloud.Games.Identity.Application.Users.Requests
{
    public class UserRoleAssingRequest
    {
        public required string Email { get; set; }
        public required UserRoleEnum Role { get; set; }
    }
}
