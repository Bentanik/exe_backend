using exe_backend.Contract.Services.Subscription;

namespace exe_backend.Application.UseCases.V1.Commands.Subscription;

public sealed class CreateSubscriptionPackageCommandHandler
    (IUnitOfWork unitOfWork)
    : ICommandHandler<Command.CreateSubscriptionPackageCommand>
{
    public async Task<Result> Handle(Command.CreateSubscriptionPackageCommand command, CancellationToken cancellationToken)
    {
        var subscriptionPackage = SubscriptionPackage.Create(Guid.NewGuid(), command.Name, command.Price, command.ExpiredMonth, command.Description);

        unitOfWork.SubscriptionPackageRepository.Add(subscriptionPackage);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(new Success(SubscriptionMessage.CreateSubscriptionMessageSuccessfully.GetMessage().Code, SubscriptionMessage.CreateSubscriptionMessageSuccessfully.GetMessage().Message));
    }
}
