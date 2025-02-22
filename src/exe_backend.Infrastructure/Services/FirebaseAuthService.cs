using Firebase.Auth;
using Firebase.Auth.Providers;

namespace exe_backend.Infrastructure.Services;

public class FirebaseAuthService : IFirebaseAuthService
{
    private readonly FirebaseAuthClient _client;
    public FirebaseAuthService(IOptions<FirebaseSetting> firebaseSetting)
    {
        var config = new FirebaseAuthConfig
        {
            ApiKey = firebaseSetting.Value.WebApiKey,
            AuthDomain = firebaseSetting.Value.AuthDomain,
            Providers =
            [
                new EmailProvider()
            ]
        };
        _client = new FirebaseAuthClient(config);
    }

    public async Task<UserCredential> RegisterWithEmailAndPasswordAsync(string email, string password)
    {
        var userCredential = await _client.CreateUserWithEmailAndPasswordAsync(email, password);
        return userCredential;
    }

    public async Task<UserCredential> SignInWithEmailAndPasswordAsync(string email, string password)
    {
        var userCredential = await _client.SignInWithEmailAndPasswordAsync(email, password);
        return userCredential;
    }
}
