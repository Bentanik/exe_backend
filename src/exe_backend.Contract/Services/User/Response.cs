using exe_backend.Contract.DTOs.UserDTOs;

namespace exe_backend.Contract.Services.User;

public static class Response
{
    public record FailPurcharseVipResponse(string FailureUrl);
    public record GetUserIdResponse(UserDto UserDto);
}