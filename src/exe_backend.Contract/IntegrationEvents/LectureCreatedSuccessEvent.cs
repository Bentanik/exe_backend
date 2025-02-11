using exe_backend.Contract.DTOs.CourseDTOs;

namespace exe_backend.Contract.IntegrationEvents;

public record LectureCreatedSuccessEvent : IntegrationEvent
{
    public LectureDTO LectureDTO { get; set; } = default!;
    public string ImageFilePath { get; set; } = default!;
    public string VideoFilePath { get; set; } = default!;
}