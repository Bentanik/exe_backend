using exe_backend.Contract.Services.User;

namespace exe_backend.Application.UseCases.V1.Commands.User;

public sealed class RenewVipCommandHandler
    (IUnitOfWork unitOfWork)
    : ICommandHandler<Command.RenewVipCommand>
{
    public async Task<Result> Handle(Command.RenewVipCommand command, CancellationToken cancellationToken)
    {
        var subscription = await unitOfWork.SubscriptionRepository
            .FindSingleAsync(s => s.UserId == command.UserId, cancellationToken, s => s.SubscriptionPackage);

        if(subscription == null)
            throw new UserException.SubscriptionCancelFailException();

        subscription.Renew();

        unitOfWork.SubscriptionRepository.Update(subscription);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(new Success(UserMessage.SubscriptionRenewSuccessfully.GetMessage().Code, UserMessage.SubscriptionRenewSuccessfully.GetMessage().Message));
    }
}