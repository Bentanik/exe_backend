namespace exe_backend.Contract.Services.Auth;

public static class Query
{
    public record LoginQuery(string IdTokenFirebase)
        : IQuery<Success<Response.LoginResponse>>;
    
     public record RefreshTokenQuery(string RefreshToken)
    : IQuery<Success<Response.LoginResponse>>;
}