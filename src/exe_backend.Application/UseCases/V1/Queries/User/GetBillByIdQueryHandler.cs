using exe_backend.Contract.Services.User;

namespace exe_backend.Application.UseCases.V1.Queries.User;
public sealed class GetBillByIdQueryHandler
    (IUnitOfWork unitOfWork)
    : IQueryHandler<Query.GetBillByIdQuery, Success<Contract.Services.User.Response.GetBillIdResponse>>
{
    public async Task<Result<Success<Contract.Services.User.Response.GetBillIdResponse>>> Handle(Query.GetBillByIdQuery query, CancellationToken cancellationToken)
    {
        var result = await unitOfWork.SubscriptionRepository
            .FindSingleAsync(sb => sb.Id == query.BillId,
                            cancellationToken, 
                            query => query.User,
                            query => query.SubscriptionPackage);

        var response = new Contract.Services.User.Response.GetBillIdResponse(result.User.FullName, (DateTime)result.SubscriptionPackage.CreatedDate, result.SubscriptionPackage.Name);
        
        return Result.Success(new Success<Contract.Services.User.Response.GetBillIdResponse>("", "", response));
    }
}
