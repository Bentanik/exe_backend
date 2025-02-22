using exe_backend.Contract.DTOs.LevelDTOs;

namespace exe_backend.Contract.Services.Level;

public static class Response
{
    public record LevelResponse
        (LevelDTO Level);

    public record LevelsResponse
     (PagedResult<LevelDTO> Levels);
}
