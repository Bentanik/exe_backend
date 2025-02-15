using exe_backend.Contract.DTOs.SubscriptionDTOs;
using exe_backend.Contract.Services.Subscription;

namespace exe_backend.Application.UseCases.V1.Queries.Level;

public sealed class GetSubscriptionPackageByIdQueryHandler
    (IUnitOfWork unitOfWork)
    : IQueryHandler<Query.GetSubscriptionPackageByIdQuery, Success<Contract.Services.Subscription.Response.SubscriptionPackageResponse>>
{
    public async Task<Result<Success<Contract.Services.Subscription.Response.SubscriptionPackageResponse>>> Handle(Query.GetSubscriptionPackageByIdQuery query, CancellationToken cancellationToken)
    {
        var result = await unitOfWork.SubscriptionPackageRepository
            .FindSingleAsync(c => c.Id == query.SubscriptionPackageId, cancellationToken);

        var subscriptionPackageDTO = result.Adapt<SubscriptionPackageDTO>();

        var response = new Contract.Services.Subscription.Response.SubscriptionPackageResponse(subscriptionPackageDTO);

        return Result.Success(new Success<Contract.Services.Subscription.Response.SubscriptionPackageResponse>(SubscriptionMessage.GetSubscriptionMessageSuccessfully.GetMessage().Code, SubscriptionMessage.GetSubscriptionMessageSuccessfully.GetMessage().Message, response));
    }
}
