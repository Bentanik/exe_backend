using exe_backend.Contract.DTOs.CourseDTOs;

namespace exe_backend.Contract.Services.Course;

public static class Command
{
    public record CreateCourseCommand(string Name, string Description, IFormFile ThumbnailFile, Guid? CategoryId = null) : ICommand;
    public record SaveThumbnailCourseCommand(CourseDTO CourseDTO) : ICommand;
    public record CreateChapterCommand(ChapterDTO ChapterDTO) : ICommand;
    public record CreateLectureCommand(LectureDTO LectureDTO, IFormFile ImageFile, IFormFile VideoFile) : ICommand;
    public record SaveImageAndVideoLectureCommand(LectureDTO LectureDTO) : ICommand;
}