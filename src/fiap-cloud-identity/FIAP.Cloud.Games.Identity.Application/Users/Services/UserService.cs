using Microsoft.AspNetCore.Identity;
using FIAP.Cloud.Games.Identity.Data.Models;
using FIAP.Cloud.Games.Identity.Application.Users.Requests;
using FIAP.Cloud.Games.Core.Responses;
using FIAP.Cloud.Games.Core.Exceptions;
using FIAP.Cloud.Games.Identity.Application.Users.Exceptions.Consts;
using FIAP.Cloud.Games.Identity.Application.Users.Exceptions.Extensions;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using FIAP.Cloud.Games.Identity.CrossCutting.Security;
using FIAP.Cloud.Games.Identity.Application.Users.Responses;
using FIAP.Cloud.Games.Identity.Application.Users.Rules;
using FIAP.Cloud.Games.Identity.Application.Users.Events.Handlers;
using FIAP.Cloud.Games.Identity.Application.Users.Events;

namespace FIAP.Cloud.Games.Identity.Application.Users.Services
{
    public class UserAppService(SignInManager<CurrentUser> signInManager, UserManager<CurrentUser> userManager, IOptions<JwtSettings> jwtSettings, UserEventHandler eventHandler)
    {
        public async Task<InternalResponse<CurrentUser>> UserCreateAsync(UserCreateRequest request)
        {
            if (request is { Email: "" or null, Name: "" or null, Password: "" or null })
                return DefaultErrorFactory.GetInvalidResource<CurrentUser>();

            var currentUser = new CurrentUser(request.Email!, request.Name!);

            var resultOfCreation = await userManager.CreateAsync(currentUser, request.Password);

            if (!resultOfCreation.Succeeded)
                return DefaultErrorFactory.GetErrors<CurrentUser>(UserErrorConst.USER_CREATE_ERROR, resultOfCreation.GetErrorsFromIdentity());

            var roleCreationResult = await userManager.AddToRoleAsync(currentUser, UserRoleEnum.User.ToString());

            if (!roleCreationResult.Succeeded)
                return DefaultErrorFactory.GetErrors<CurrentUser>(UserErrorConst.USER_ROLE_ASSIGN_ERROR, roleCreationResult.GetErrorsFromIdentity());

            await eventHandler.Handle(new UserCreatedEvent(currentUser.Id));

            return new InternalResponse<CurrentUser>(currentUser);
        }

        public async Task<InternalResponse<CurrentUser>> UserRoleAssignAsync(UserRoleAssingRequest request)
        {
            if (!Enum.IsDefined(typeof(UserRoleEnum), request.Role) && request is { Email: "" or null })
                return DefaultErrorFactory.GetInvalidResource<CurrentUser>();

            var user = await userManager.FindByEmailAsync(request.Email!);

            if (user is null)
                return DefaultErrorFactory.GetNotFound<CurrentUser>();

            var roleExistsResult = await userManager.IsInRoleAsync(user, request.Role.ToString());

            if (roleExistsResult)
                return DefaultErrorFactory.GetErrors<CurrentUser>(UserErrorConst.USER_ROLE_ALREADY_ASSIGNED);

            var roleCreationResult = await userManager.AddToRoleAsync(user, request.Role.ToString());

            if (!roleCreationResult.Succeeded)
                return DefaultErrorFactory.GetErrors<CurrentUser>(UserErrorConst.USER_ROLE_ASSIGN_ERROR, roleCreationResult.GetErrorsFromIdentity());

            return new InternalResponse<CurrentUser>(user);
        }

        public async Task<InternalResponse<UserAuthenticateResponse>> UserAuthenticateAsync(UserAuthenticateRequest request)
        {
            if (request is { Email: "" or null, Password: "" or null })
                return DefaultErrorFactory.GetInvalidResource<UserAuthenticateResponse>();

            var resultOfSignIn = await signInManager.PasswordSignInAsync(request.Email, request.Password, false, true);

            if (!resultOfSignIn.Succeeded)
                return DefaultErrorFactory.GetErrors<UserAuthenticateResponse>(UserErrorConst.USER_SIGN_IN_ERROR, resultOfSignIn.GetErrorsFromIdentity());

            var user = await userManager.FindByEmailAsync(request.Email!);

            if (user is null)
                return DefaultErrorFactory.GetNotFound<UserAuthenticateResponse>();

            var claims = await userManager.GetClaimsAsync(user);
            var userRoles = await userManager.GetRolesAsync(user);

            var settings = jwtSettings.Value;

            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id));
            claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email!));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Nbf, DateTimeOffset.Now.ToUnixTimeSeconds().ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.Now.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64));

            foreach (var roles in userRoles)
            {
                claims.Add(new Claim("role", roles));
            }

            var identityClaims = new ClaimsIdentity();
            identityClaims.AddClaims(claims);

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(settings.SECRET);

            var token = tokenHandler.CreateToken(new SecurityTokenDescriptor()
            {
                Issuer = settings.ISSUER,
                Audience = settings.VALID_AT,
                Subject = identityClaims,
                Expires = DateTime.UtcNow.AddHours(double.Parse(settings.EXPIRES_IN_HOURS)),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            });

            var encodedToken = tokenHandler.WriteToken(token);

            return new InternalResponse<UserAuthenticateResponse>(new UserAuthenticateResponse
            {
                AccessToken = encodedToken,
                ExpiresIn = TimeSpan.FromHours(double.Parse(settings.EXPIRES_IN_HOURS)).TotalSeconds
            });
        }
    }
}