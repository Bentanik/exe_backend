using exe_backend.Contract.DTOs.CourseDTOs;

namespace exe_backend.Contract.Services.Course;

public static class Command
{
    public record CreateCourseCommand(string Name, string Description, IFormFile ThumbnailFile, Guid? CategoryId = null, Guid? LevelId = null, Guid[]? ChapterIds = null) : ICommand;
    public record SaveThumbnailCourseCommand(CourseDTO CourseDTO) : ICommand;
    public record CreateChapterCommand(Guid CourseId, string Description, string Name, Guid[]? LectureIds = null) : ICommand;
    public record CreateLectureCommand(LectureDTO LectureDTO, IFormFile ImageFile) : ICommand;
    public record SaveImageLectureCommand(LectureDTO LectureDTO) : ICommand;
}