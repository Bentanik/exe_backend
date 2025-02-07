using exe_backend.Contract.Abstractions.Shared;

namespace exe_backend.Contract.Services.Auth;

public static class Query
{
    public record LoginQuery(string Email, string Password)
        : IQuery<Success<Response.LoginResponse>>;
}