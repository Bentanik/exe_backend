using exe_backend.Contract.DTOs.CourseDTOs;

namespace exe_backend.Contract.Services.Course;

public static class Event
{
    public record CreatedCourseEvent(Guid Id, CourseDTO CourseDto, IFormFile ThumbnailFile) : IDomainEvent;
}