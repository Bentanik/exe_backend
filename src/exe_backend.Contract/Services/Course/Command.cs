using exe_backend.Contract.DTOs.CourseDTOs;

namespace exe_backend.Contract.Services.Course;

public static class Command
{
    public record CreateCourseCommand(string Name, string Description, IFormFile ThumbnailFile) : ICommand;

    public record SaveThumbnailCourseCommand(CourseDTO CourseDTO) : ICommand;
}