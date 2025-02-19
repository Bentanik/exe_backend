
using System.Net.Http.Json;
using System.Text.Json.Serialization;

namespace exe_backend.Infrastructure.Services;

public class JwtProviderService
    (HttpClient httpClient)
    : IJwtProviderService
{
    public async Task<string> GetForCredentialAsync(string email, string password)
    {
        var request = new
        {
            email,
            password,
            returnSecureToken = true,
        };

        var response = await httpClient.PostAsJsonAsync("", request);
        var authToken = await response.Content.ReadFromJsonAsync<AuthToken>();
        
        return authToken.IdToken;
    }

    public class AuthToken
    {
        [JsonPropertyName("kind")]
        public string Kind { get; set; }

        [JsonPropertyName("localId")]
        public string LocalId { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("displayname")]
        public string DisplayName { get; set; }

        [JsonPropertyName("idToken")]
        public string IdToken { get; set; }

        [JsonPropertyName("registered")]
        public string Registered { get; set; }

        [JsonPropertyName("refreshToken")]
        public string refreshToken { get; set; }

        [JsonPropertyName("expiresIn")]
        public string ExpiresIn { get; set; }
    }
}