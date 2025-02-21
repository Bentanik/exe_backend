using exe_backend.Contract.DTOs.AuthDTOs;
using exe_backend.Contract.Services.Auth;
using Firebase.Auth;
using FirebaseAdmin.Auth;
using System.Security.Claims;

namespace exe_backend.Application.UseCases.V1.Queries.Auth;

public sealed class LoginQueryHandler
    (IUnitOfWork unitOfWork, IFirebaseAuthService firebaseAuthService)
    : IQueryHandler<Query.LoginQuery, Success<Contract.Services.Auth.Response.LoginResponse>>
{
    public async Task<Result<Success<Contract.Services.Auth.Response.LoginResponse>>> Handle(Query.LoginQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var decodedToken = await FirebaseAuth.DefaultInstance.VerifyIdTokenAsync(query.IdTokenFirebase, cancellationToken);

            var userIdentityId = decodedToken.Uid;

            var user = await unitOfWork.UserRepository.FindSingleAsync(u => u.IdentityId == userIdentityId, includeProperties: u => u.Role);

            var claims = new Dictionary<string, object>
            {
                { "UserId", user.Id },
                { ClaimTypes.Role, user.Role.Name}
            };

            await FirebaseAuth.DefaultInstance.SetCustomUserClaimsAsync(userIdentityId, claims, cancellationToken);

            var authTokenDto = new AuthTokenDTO(query.IdTokenFirebase, null, AuthConstant.BearerTokenScheme);
            var authUserDto = new AuthUserDTO(user.Email, user.FullName, user.PublicAvatarUrl, user.Role.Name);

            var loginDto = new LoginDTO(authTokenDto, authUserDto);
            var response = new Contract.Services.Auth.Response.LoginResponse(loginDto);

            return Result.Success(new Success<Contract.Services.Auth.Response.LoginResponse>
            (AuthMessage.LoginSuccessfully.GetMessage().Code,
             AuthMessage.LoginSuccessfully.GetMessage().Message, response));

        }
        catch (FirebaseAuthHttpException ex)
        {
            System.Console.WriteLine(ex.Message.ToString());
            throw new AuthException.LoginWithEmailAndPasswordException();
        }
    }
}
