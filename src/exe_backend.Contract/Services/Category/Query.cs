namespace exe_backend.Contract.Services.Category;

public static class Query {
    public record GetCategoriesQuery(string? SearchTerm, string? SortColumn, SortOrder? SortOrder, string?[] IncludesProperty, int PageIndex, int PageSize) : IQuery<Success<Response.CategoriesResponse>>;
}