namespace exe_backend.Contract.Services.Course;

public static class Query
{
    public record GetCoursesQuery(string? SearchTerm, string? SortColumn, SortOrder? SortOrder, string?[] IncludesProperty, int PageIndex, int PageSize) : IQuery<Success<Response.CoursesResponse>>;
}