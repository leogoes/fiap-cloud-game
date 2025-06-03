using FIAP.Cloud.Games.Core.Exceptions;
using FIAP.Cloud.Games.Identity.Application.Users.Exceptions.Consts;
using Microsoft.AspNetCore.Identity;

namespace FIAP.Cloud.Games.Identity.Application.Users.Exceptions.Extensions
{
    public static class IdentityErrorExtension
    {
        public static ErrorDetail GetErrorsFromIdentity(this IdentityError result)
        {
            return new ErrorDetail
            {
                Slug = result.Code,
                Message = result.Description
            };
        }

        public static IEnumerable<ErrorDetail> GetErrorsFromIdentity(this IdentityResult result)
        {
            return result.Errors.Select(GetErrorsFromIdentity);
        }

        public static IEnumerable<ErrorDetail> GetErrorsFromIdentity(this SignInResult result)
        {
            if (result.IsNotAllowed)
                return [
                    new ErrorDetail
                    {
                        Slug = UserErrorConst.USER_NOT_ALLOWED.Key,
                        Message = UserErrorConst.USER_NOT_ALLOWED.Value
                    }
                ];

            if (!result.Succeeded)
                return [
                    new ErrorDetail
                    {
                        Slug = UserErrorConst.USER_AUTHENTICATION_FAILED.Key,
                        Message = UserErrorConst.USER_AUTHENTICATION_FAILED.Value
                    }
                ];

            if (result.IsLockedOut)
                return [
                    new ErrorDetail
                    {
                        Slug = UserErrorConst.USER_AUTHENTICATION_FAILED.Key,
                        Message = UserErrorConst.USER_AUTHENTICATION_FAILED.Value
                    }
                ];

            return [
                new ErrorDetail
                {
                    Slug = UserErrorConst.USER_SIGN_IN_ERROR.Key,
                    Message = UserErrorConst.USER_SIGN_IN_ERROR.Value
                }
            ];
        }
    }
}
