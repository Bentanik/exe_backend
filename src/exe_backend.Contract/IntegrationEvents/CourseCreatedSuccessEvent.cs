using exe_backend.Contract.DTOs.CourseDTOs;

namespace exe_backend.Contract.IntegrationEvents;

public record CourseCreatedSuccessEvent : IntegrationEvent
{
    public CourseDTO CourseDTO { get; set; } = default!;
    public string FilePath { get; set; } = default!;
}