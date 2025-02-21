using FirebaseAdmin;
using FirebaseAdmin.Auth;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text.Encodings.Web;

namespace exe_backend.Api.DepedencyInjection.Extensions;

public class FirebaseAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{
    private readonly FirebaseApp _firebaseApp;

    public FirebaseAuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock, FirebaseApp firebaseApp) : base(options, logger, encoder, clock)
    {
        _firebaseApp = firebaseApp;
    }

    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        if(!Context.Request.Headers.ContainsKey("Authorization"))
        {
            return AuthenticateResult.NoResult();
        }

        string bearerToken = Context.Request.Headers["Authorization"];

        if(bearerToken == null || !bearerToken.StartsWith("Bearer "))
        {
            return AuthenticateResult.Fail("Invalid scheme");
        }

        string token = bearerToken["Bearer ".Length..];

        try
        {
            var firebaseToken = await FirebaseAuth.GetAuth(_firebaseApp).VerifyIdTokenAsync(token);

            return AuthenticateResult.Success(new AuthenticationTicket(new ClaimsPrincipal(
            [
                new(ToClaims(firebaseToken.Claims), nameof(FirebaseAuthenticationHandler))
            ]), JwtBearerDefaults.AuthenticationScheme));
        }
        catch (Exception ex)
        {
            return AuthenticateResult.Fail(ex);
        }

    }

    private IEnumerable<Claim> ToClaims(IReadOnlyDictionary<string, object> claims) => [
            new("IdentityId", claims["user_id"].ToString()),
            new(ClaimTypes.Role, claims[ClaimTypes.Role].ToString()),
            new("UserId", claims["UserId"].ToString()),
        ];
}
