namespace exe_backend.Contract.DTOs.UserDTOs;

public record UserDto(Guid? Id, string? Email, string? Password, string? FullName, string? Avatar);