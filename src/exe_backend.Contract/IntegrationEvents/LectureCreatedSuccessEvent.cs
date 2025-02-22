using exe_backend.Contract.DTOs.CourseDTOs;

namespace exe_backend.Contract.IntegrationEvents;

public record LectureCreatedSuccessEvent : IntegrationEvent
{
    public LectureDTO LectureDTO { get; set; } = default!;
    public byte[] ImageFilePath { get; set; } = default!;
}