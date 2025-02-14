namespace exe_backend.Contract.Services.Level;

public static class Query
{
    public record GetLevelsQuery(string? SearchTerm, string? SortColumn, SortOrder? SortOrder, string?[] IncludesProperty, int PageIndex, int PageSize) : IQuery<Success<Response.LevelsResponse>>;

    public record GetLevelByIdQuery(Guid? LevelId, string?[] IncludesProperty) : IQuery<Success<Response.LevelResponse>>;
}