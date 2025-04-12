namespace exe_backend.Contract.Services.Donate;

public static class Query
{
    public record GetDonatesQuery(string? SearchTerm, string? SortColumn, SortOrder? SortOrder, string?[] IncludesProperty, int PageIndex, int PageSize)
        : IQuery<Success<Response.DonatesResponse>>;
}
