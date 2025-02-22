using Firebase.Auth;

namespace exe_backend.Contract.Infrastructure.Services;


public interface IFirebaseAuthService
{
    Task<UserCredential> RegisterWithEmailAndPasswordAsync(string email, string password);
    Task<UserCredential> SignInWithEmailAndPasswordAsync(string email, string password);
}