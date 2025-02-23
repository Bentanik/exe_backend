using exe_backend.Contract.DTOs.SubscriptionDTOs;
using exe_backend.Contract.DTOs.UserDTOs;
using exe_backend.Contract.Services.User;

namespace exe_backend.Application.UseCases.V1.Queries.User;

public sealed class GetInfoUserByIdQueryHandler
    (IUnitOfWork unitOfWork)
    : IQueryHandler<Query.GetUserByIdQuery, Success<Contract.Services.User.Response.GetUserIdResponse>>
{
    public async Task<Result<Success<Contract.Services.User.Response.GetUserIdResponse>>> Handle(Query.GetUserByIdQuery query, CancellationToken cancellationToken)
    {
        var includes = query.IncludesProperty?.Select(include =>
                {
                    return include switch
                    {
                        "Subscription" => (Expression<Func<Domain.Models.User, object>>)(c => c.Subscription),
                        _ => throw new ArgumentException($"Unknown navigation property: {include}")
                    };
                }).ToArray();

        var user = await unitOfWork.UserRepository.FindSingleAsync(u => u.Id == query.UserId, cancellationToken, includeProperties: includes);

        var userDto = user.Adapt<UserDto>();

        if (query.IncludesProperty.Contains("Subscription") && user.Subscription != null)
        {

            var subscriptionPackage = await unitOfWork.SubscriptionPackageRepository.FindSingleAsync(sb => sb.Id == user.Subscription.SubscriptionPackageId);

            userDto = userDto with
            {
                SubscriptionPackage = subscriptionPackage.Adapt<SubscriptionPackageDTO>()
            };
        }


        var response = new Contract.Services.User.Response.GetUserIdResponse(userDto);

        return Result.Success(new Success<Contract.Services.User.Response.GetUserIdResponse>(UserMessage.GetInfoUserSuccessfully.GetMessage().Code, UserMessage.GetInfoUserSuccessfully.GetMessage().Message, response));
    }
}
