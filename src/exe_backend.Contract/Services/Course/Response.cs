using exe_backend.Contract.DTOs.CourseDTOs;

namespace exe_backend.Contract.Services.Course;

public static class Response
{
    public record CoursesResponse(PagedResult<CourseDTO> Courses);
}