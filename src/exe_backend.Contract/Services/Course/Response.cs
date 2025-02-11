using exe_backend.Contract.DTOs.CourseDTOs;

namespace exe_backend.Contract.Services.Course;

public static class Response
{
    public record CoursesResponse(PagedResult<CourseDTO> Courses);
    public record CourseResponse(CourseDTO Course);
    public record ChaptersResponse(PagedResult<ChapterDTO> Chapters);
    public record ChapterResponse(ChapterDTO Chapter);
    public record LecturesResponse(PagedResult<LectureDTO> Lectures);

}