namespace exe_backend.Contract.Services.Subscription;

public static class Query
{
    public record GetSubscriptionPackagesQuery(string? SearchTerm, string? SortColumn, SortOrder? SortOrder, string?[] IncludesProperty, int PageIndex, int PageSize) : IQuery<Success<Response.SubscriptionPackagesResponse>>;

    public record GetSubscriptionPackageByIdQuery(Guid? SubscriptionPackageId, string?[] IncludesProperty) : IQuery<Success<Response.SubscriptionPackageResponse>>;
}