namespace exe_backend.Contract.Services.User;

public static class Query
{
    public record GetUserByIdQuery(Guid UserId, string?[] IncludesProperty) : IQuery<Success<Response.GetUserIdResponse>>;
    public record GetBillByIdQuery(Guid BillId) : IQuery<Success<Response.GetBillIdResponse>>;
    public record GetUsersQuery(string? SearchTerm, string? SortColumn, SortOrder? SortOrder, string?[] IncludesProperty, int PageIndex, int PageSize)
     : IQuery<Success<Response.UsersResponse>>;
}