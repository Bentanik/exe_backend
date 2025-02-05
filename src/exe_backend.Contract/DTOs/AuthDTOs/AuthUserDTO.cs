namespace exe_backend.Contract.DTOs.AuthDTOs;
public record AuthUserDTO(
    string? Email = null,
    string? FullName = null,
    string? AvatarUrl = null
);

