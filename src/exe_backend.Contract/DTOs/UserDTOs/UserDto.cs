namespace exe_backend.Contract.DTOs.UserDTOs;

public record UserDto(Guid? Id = null, string? Email = null, string? IdentityId = null, string? FullName = null, string? Avatar = null);