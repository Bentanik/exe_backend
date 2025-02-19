namespace exe_backend.Contract.Infrastructure.Services;

public interface IJwtProviderService
{
    Task<string> GetForCredentialAsync(string email, string password);
}