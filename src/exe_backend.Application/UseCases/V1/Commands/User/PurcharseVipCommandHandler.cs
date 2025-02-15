using exe_backend.Contract.Services.User;

namespace exe_backend.Application.UseCases.V1.Commands.User;

public sealed class PurcharseVipCommandHandler
    (IUnitOfWork unitOfWork)
    : ICommandHandler<Command.PurcharseVipCommand>
{
    public async Task<Result> Handle(Command.PurcharseVipCommand command, CancellationToken cancellationToken)
    {
        var isCheckSubscription = await unitOfWork.SubscriptionRepository
            .FindSingleAsync(s => s.UserId == command.UserId);

        // If Subscription found by UserId have IsActive = true(subscription not expired date) => Exception
        if (isCheckSubscription != null)
        {
            throw new UserException.SubscriptionActivedException();
        }

        var user = await unitOfWork.UserRepository
            .FindSingleAsync(u => u.Id == command.UserId);

        var subscriptionPackage = await unitOfWork.SubscriptionPackageRepository.
            FindSingleAsync(sb => sb.Id == command.SubscriptionPackageId);

        var subscription = Domain.Models.Subscription.Create(Guid.NewGuid(), subscriptionPackage, command.UserId);

        user.AddSubscription(subscription);

        unitOfWork.UserRepository.Update(user);
        unitOfWork.SubscriptionRepository.Add(subscription);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(new Success(UserMessage.SubscriptionActivedSuccessfully.GetMessage().Code, UserMessage.SubscriptionActivedSuccessfully.GetMessage().Message));
    }
}