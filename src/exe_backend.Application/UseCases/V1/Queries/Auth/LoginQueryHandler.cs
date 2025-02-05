using exe_backend.Contract.DTOs.AuthDTOs;
using exe_backend.Contract.Services.Auth;

namespace exe_backend.Application.UseCases.V1.Queries.Auth;

public sealed class LoginQueryHandler
    (IUnitOfWork unitOfWork, IPasswordHashService passwordHashService, ITokenGeneratorService tokenGenenratorService)
    : IQueryHandler<Query.LoginQuery, Success<Response.LoginResponse>>
{
    public async Task<Result<Success<Response.LoginResponse>>> Handle(Query.LoginQuery query, CancellationToken cancellationToken)
    {
        // Get user by email
        var user = await unitOfWork.UserRepository
            .FindSingleAsync(predicate: u => u.Email == query.Email,
                            cancellationToken: cancellationToken);

        // If user not exist
        if (user == null)
            throw new AuthException.UserNotExistException();

        // Check password
        var isCheckPassword = passwordHashService.VerifyPassword(query.Password, user.Password);

        // If password not match
        if (isCheckPassword == false)
            throw new AuthException.PasswordNotMatchException();

        // Genenrate token
        var accessToken = tokenGenenratorService.GenerateAccessToken(user.Id, 1);
        var refreshToken = tokenGenenratorService.GenerateRefreshToken(user.Id, 2);

        var authTokenDto = new AuthTokenDTO(accessToken, refreshToken, AuthConstant.BearerTokenScheme);

        var authUserDto = new AuthUserDTO(user.Email, user.FullName, user.PublicAvatarUrl);

        //  Response
        var loginDto = new LoginDTO(authTokenDto, authUserDto);

        var response = new Response.LoginResponse(loginDto);

        return Result.Success(new Success<Response.LoginResponse>
        (AuthMessage.LoginSuccessfully.GetMessage().Code,
         AuthMessage.LoginSuccessfully.GetMessage().Message, response));
    }
}
