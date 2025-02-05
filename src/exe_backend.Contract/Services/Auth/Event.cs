using exe_backend.Contract.DTOs.UserDTOs;

namespace exe_backend.Contract.Services.Auth;

public static class Event
{
    public record UserRegisterdEvent(Guid Id, UserDto UserDto) : IDomainEvent;
    public record UserPasswordResetConfirmedEvent(Guid Id, UserDto UserDto, string Token) : IDomainEvent;
}