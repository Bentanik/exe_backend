using exe_backend.Contract.DTOs.MediaDTOs;
using exe_backend.Domain.ValueObjects;

namespace exe_backend.Application.Mapping;

public class MappingConfig
{
    public static void Configure()
    {
        // Map from Image to ImageDTO
        TypeAdapterConfig<Image, ImageDTO>.NewConfig()
            .Map(dest => dest.PublicId, src => src.PublicId)
            .Map(dest => dest.PublicUrl, src => src.PublicUrl);
    }
}