namespace exe_backend.Contract.DTOs.AuthDTOs;
public record LoginDTO(
    AuthTokenDTO AuthTokenDTO,
    AuthUserDTO AuthUserDTO
);