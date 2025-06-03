namespace FIAP.Cloud.Games.Identity.Application.Users.Exceptions.Consts
{
    public class UserErrorConst
    {
        public static readonly KeyValuePair<string, string> USER_ROLE_ALREADY_ASSIGNED = new("USER_ROLE_ALREADY_ASSIGNED", "Role already assigned to user.");
        public static readonly KeyValuePair<string, string> USER_ROLE_ASSIGN_ERROR = new("USER_ROLE_ASSIGN_ERROR", "Unable to assing role to user.");
        public static readonly KeyValuePair<string, string> USER_CREATE_ERROR = new("USER_CREATE_ERROR", "Unable to create user.");
        public static readonly KeyValuePair<string, string> USER_SIGN_IN_ERROR = new("USER_SIGN_IN_ERROR", "Unable to sign in with this user.");
        public static readonly KeyValuePair<string, string> USER_NOT_FOUND = new("USER_NOT_FOUND", "User not found.");
        public static readonly KeyValuePair<string, string> USER_LOCKED_OUT = new("USER_LOCKED_OUT", "User is locked out.");
        public static readonly KeyValuePair<string, string> USER_AUTHENTICATION_FAILED = new("USER_AUTHENTICATION_FAILED", "User authenticaion failed.");
        public static readonly KeyValuePair<string, string> USER_NOT_ALLOWED = new("USER_NOT_ALLOWED", "User is not allowed.");
    }
}
