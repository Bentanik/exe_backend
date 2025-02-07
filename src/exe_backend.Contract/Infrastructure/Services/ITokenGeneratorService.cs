namespace exe_backend.Contract.Infrastructure.Services;

public interface ITokenGeneratorService
{
    string GenerateToken
        (string secretKey, string issuer, string audience, double expirationMinutes, IEnumerable<Claim>? claims = null);
    string GenerateAccessToken(Guid userId, int roleName);
    string GenerateRefreshToken(Guid userId, int roleName);
    string ValidateAndGetUserIdFromRefreshToken(string refreshToken);
    string GenerateForgotPasswordToken(Guid userId);
    string ValidateAndGetUserIdForgotPasswordToken(string forgotPasswordToken);
}
