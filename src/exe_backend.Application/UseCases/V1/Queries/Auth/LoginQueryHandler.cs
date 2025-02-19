using exe_backend.Contract.Services.Auth;

namespace exe_backend.Application.UseCases.V1.Queries.Auth;

public sealed class LoginQueryHandler
    (IJwtProviderService jwtProviderService)
    : IQueryHandler<Query.LoginQuery, Success<Contract.Services.Auth.Response.LoginResponse>>
{
    public async Task<Result<Success<Contract.Services.Auth.Response.LoginResponse>>> Handle(Query.LoginQuery query, CancellationToken cancellationToken)
    {
        var token = await jwtProviderService.GetForCredentialAsync(query.Email, query.Password);
        
        return Result.Success(new Success<Contract.Services.Auth.Response.LoginResponse>
        (AuthMessage.LoginSuccessfully.GetMessage().Code,
         AuthMessage.LoginSuccessfully.GetMessage().Message, null));
    }
}
