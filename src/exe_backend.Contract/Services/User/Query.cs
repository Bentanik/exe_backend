namespace exe_backend.Contract.Services.User;

public static class Query
{
    public record GetUserByIdQuery(Guid UserId, string?[] IncludesProperty) : IQuery<Success<Response.GetUserIdResponse>>;
    public record GetBillByIdQuery(Guid BillId) : IQuery<Success<Response.GetBillIdResponse>>;
}