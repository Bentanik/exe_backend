using exe_backend.Contract.DTOs.CourseDTOs;
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

        // Map from Video to VideoDTO
        TypeAdapterConfig<Video, VideoDTO>.NewConfig()
            .Map(dest => dest.PublicId, src => src.PublicId)
            .Map(dest => dest.Duration, src => src.Duration);

        TypeAdapterConfig<Course, CourseDTO>.NewConfig()
            .Map(dest => dest.Category, src => src.Category)
            .Map(dest => dest.Level, src => src.Level);

    }
}