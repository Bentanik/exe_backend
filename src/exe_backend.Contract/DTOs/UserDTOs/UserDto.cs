namespace exe_backend.Contract.DTOs.UserDTOs;

public record UserDto(Guid? Id = null, string? IdentityId = null, string? Email = null, string? FullName = null, string? Avatar = null);