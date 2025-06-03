using FIAP.Cloud.Games.Identity.Application.Users.Rules;

namespace FIAP.Cloud.Games.Identity.Application.Users.Extensions
{
    public static class UserRoleExtensions
    {
        public static string GetRoleName(this UserRoleEnum role)
        {
            return role switch
            {
                UserRoleEnum.Admin => UserRoleConst.Admin,
                UserRoleEnum.User => UserRoleConst.User,
                _ => throw new ArgumentOutOfRangeException(nameof(role), role, null)
            };
        }
    }
}
