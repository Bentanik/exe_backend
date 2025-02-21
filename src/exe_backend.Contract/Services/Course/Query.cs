namespace exe_backend.Contract.Services.Course;

public static class Query
{
    public record GetCoursesQuery(string? SearchTerm, string? SortColumn, SortOrder? SortOrder, string?[] IncludesProperty, int PageIndex, int PageSize) : IQuery<Success<Response.CoursesResponse>>;

    public record GetCourseByIdQuery(Guid? CourseId, string?[] IncludesProperty, Guid? UserId = null) : IQuery<Success<Response.CourseResponse>>;

    public record GetChaptersQuery(string? SearchTerm, string? SortColumn, SortOrder? SortOrder, string?[] IncludesProperty, int PageIndex, int PageSize, bool? NoneAssignedCourse = false) : IQuery<Success<Response.ChaptersResponse>>;

    public record GetChapterByIdQuery(Guid? ChapterId, string?[] IncludesProperty) : IQuery<Success<Response.ChapterResponse>>;

    public record GetLecturesQuery(string? SearchTerm, string? SortColumn, SortOrder? SortOrder, string?[] IncludesProperty, int PageIndex, int PageSize, bool? NoneAssignedChapter = false) : IQuery<Success<Response.LecturesResponse>>;

    public record GetLectureByIdQuery(Guid? LectureId, string?[] IncludesProperty) : IQuery<Success<Response.LectureResponse>>;
}