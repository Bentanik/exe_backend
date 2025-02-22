using exe_backend.Contract.Services.User;

namespace exe_backend.Application.UseCases.V1.Commands.User;

public sealed class CancelVipCommandHandler
    (IUnitOfWork unitOfWork)
    : ICommandHandler<Command.CancelVipCommand>
{
    public async Task<Result> Handle(Command.CancelVipCommand command, CancellationToken cancellationToken)
    {
        var subscription = await unitOfWork.SubscriptionRepository
            .FindSingleAsync(s => s.UserId == command.UserId);

        // If Subscription not found => Exception
        if (subscription == null)
        {
            throw new UserException.SubscriptionCancelFailException();
        }

        unitOfWork.SubscriptionRepository.Remove(subscription);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(new Success(UserMessage.SubscriptionCancelSuccessfully.GetMessage().Code, UserMessage.SubscriptionCancelSuccessfully.GetMessage().Message));
    }
}