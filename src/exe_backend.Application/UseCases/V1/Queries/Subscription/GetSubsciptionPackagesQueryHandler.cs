using exe_backend.Contract.DTOs.SubscriptionDTOs;
using exe_backend.Contract.Services.Subscription;

namespace exe_backend.Application.UseCases.V1.Queries.Subscription;

public sealed class GetSubsciptionPackagesQueryHandler
    (IUnitOfWork unitOfWork)
    : IQueryHandler<Query.GetSubscriptionPackagesQuery, Success<Contract.Services.Subscription.Response.SubscriptionPackagesResponse>>
{
    public async Task<Result<Success<Contract.Services.Subscription.Response.SubscriptionPackagesResponse>>> Handle(Query.GetSubscriptionPackagesQuery query, CancellationToken cancellationToken)
    {
        //Find sort property without Id
        var subscriptionPackagesQuery = string.IsNullOrWhiteSpace(query.SearchTerm)
            ? unitOfWork.SubscriptionPackageRepository.FindAll() : unitOfWork.SubscriptionPackageRepository.FindAll(x => x.Name.Contains(query.SearchTerm));

        // Get sort follow property
        Expression<Func<Domain.Models.SubscriptionPackage, object>> keySelector = query.SortColumn?.ToLower() switch
        {
            "name" => level => level.Name,
            _ => level => level.CreatedDate!,
        };

        subscriptionPackagesQuery = query.SortOrder == SortOrder.Descending
             ? subscriptionPackagesQuery.OrderByDescending(keySelector) : subscriptionPackagesQuery.OrderBy(keySelector);

        var pagedResultCourse = await PagedResult<Domain.Models.SubscriptionPackage>.CreateAsync(subscriptionPackagesQuery, query.PageIndex, query.PageSize);

        var subscriptionPackageDTO = pagedResultCourse.Items.Adapt<List<SubscriptionPackageDTO>>();

        var pagedResultSubscriptionPackageDto = PagedResult<SubscriptionPackageDTO>.Create(subscriptionPackageDTO, pagedResultCourse.PageIndex, pagedResultCourse.PageSize, pagedResultCourse.TotalCount);

        var response = new Contract.Services.Subscription.Response.SubscriptionPackagesResponse(pagedResultSubscriptionPackageDto);

        return Result.Success(new Success<Contract.Services.Subscription.Response.SubscriptionPackagesResponse>(SubscriptionMessage.GetSubscriptionMessageSuccessfully.GetMessage().Code, SubscriptionMessage.GetSubscriptionMessageSuccessfully.GetMessage().Message, response));
    }
}
