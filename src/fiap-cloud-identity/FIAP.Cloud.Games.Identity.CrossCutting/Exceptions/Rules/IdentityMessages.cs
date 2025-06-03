using Microsoft.AspNetCore.Identity;

namespace FIAP.Cloud.Games.Identity.CrossCutting.Exceptions.Rules
{
    public class IdentityMessages : IdentityErrorDescriber
    {
        public override IdentityError DefaultError() { return new IdentityError { Code = "DEFAULT_ERROR", Description = $"An unknown error occurred." }; }
        public override IdentityError ConcurrencyFailure() { return new IdentityError { Code = "CONCURRENCY_FAILURE", Description = "Optimistic concurrency failure, object has been modified." }; }
        public override IdentityError PasswordMismatch() { return new IdentityError { Code = "PASSWORD_MISMATCH", Description = "Incorrect password." }; }
        public override IdentityError InvalidToken() { return new IdentityError { Code = "INVALID_TOKEN", Description = "Invalid token." }; }
        public override IdentityError LoginAlreadyAssociated() { return new IdentityError { Code = "LOGIN_ALREADY_ASSOCIATED", Description = "There is already a user with this login." }; }
        public override IdentityError InvalidUserName(string userName) { return new IdentityError { Code = "INVALID_USER_NAME", Description = $"User name '{userName}' is invalid, can only contain letters or digits." }; }
        public override IdentityError InvalidEmail(string email) { return new IdentityError { Code = "INVALID_EMAIL", Description = $"Email '{email}' is invalid." }; }
        public override IdentityError DuplicateUserName(string userName) { return new IdentityError { Code = "DUPLICATE_USER_NAME", Description = $"User name '{userName}' is already taken." }; }
        public override IdentityError DuplicateEmail(string email) { return new IdentityError { Code = "DUPLICATE_EMAIL", Description = $"Email '{email}' is already taken." }; }
        public override IdentityError InvalidRoleName(string role) { return new IdentityError { Code = "INVALID_ROLE_NAME", Description = $"Role name '{role}' is invalid." }; }
        public override IdentityError DuplicateRoleName(string role) { return new IdentityError { Code = "DUPLICATE_ROLE_NAME", Description = $"Role name '{role}' is already taken." }; }
        public override IdentityError UserAlreadyHasPassword() { return new IdentityError { Code = "USER_ALREADY_HAS_PASSWORD", Description = "User already has a password set." }; }
        public override IdentityError UserLockoutNotEnabled() { return new IdentityError { Code = "USER_LOCKOUT_NOT_ENABLED", Description = "Lockout is not enabled for this user." }; }
        public override IdentityError UserAlreadyInRole(string role) { return new IdentityError { Code = "USER_ALREADY_IN_ROLE", Description = $"User '{role}' already has this role." }; }
        public override IdentityError UserNotInRole(string role) { return new IdentityError { Code = "USER_NOT_IN_ROLE", Description = $"User does not have the role '{role}'." }; }
        public override IdentityError PasswordTooShort(int length) { return new IdentityError { Code = "PASSWORD_TOO_SHORT", Description = $"Passwords must be at least {length} characters." }; }
        public override IdentityError PasswordRequiresNonAlphanumeric() { return new IdentityError { Code = "PASSWORD_REQUIRES_NON_ALPHANUMERIC", Description = "Passwords must have at least one non-alphanumeric character." }; }
        public override IdentityError PasswordRequiresDigit() { return new IdentityError { Code = "PASSWORD_REQUIRES_DIGIT", Description = "Passwords must have at least one digit ('0'-'9')." }; }
        public override IdentityError PasswordRequiresLower() { return new IdentityError { Code = "PASSWORD_REQUIRES_LOWER", Description = "Passwords must have at least one lowercase ('a'-'z')." }; }
        public override IdentityError PasswordRequiresUpper() { return new IdentityError { Code = "PASSWORD_REQUIRES_UPPER", Description = "Passwords must have at least one uppercase ('A'-'Z')." }; }
    }
}
