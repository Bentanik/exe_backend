using exe_backend.Contract.DTOs.AuthDTOs;

namespace exe_backend.Contract.Services.Auth;

public static class Response
{
    public record LoginResponse
        (LoginDTO LoginDto);
}
