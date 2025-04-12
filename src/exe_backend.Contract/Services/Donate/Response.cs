using exe_backend.Contract.DTOs.DonateDTOs;

namespace exe_backend.Contract.Services.Donate;

public static class Response
{
    public record DonatesResponse(PagedResult<DonateDto> Donates);
}
