using exe_backend.Contract.Services.User;

namespace exe_backend.Application.UseCases.V1.Commands.User;

public sealed class SuccessPurcharseVipCommandHandler
    (IUnitOfWork unitOfWork, IResponseCacheService responseCacheService, IOptions<ClientSetting> ClientSetting)
    : ICommandHandler<Command.SuccessPurcharseVipCommand, string>
{
    public async Task<Result<string>> Handle(Command.SuccessPurcharseVipCommand command, CancellationToken cancellationToken)
    {
        //Get infomation saved in memory
       var purcharseVipCommandMemory = await responseCacheService.GetCacheResponseAsync($"subscribe_{command.OrderId}");

        // Conver JSON to object
        var purcharseVipCommand = JsonConvert.DeserializeObject<Command.PurcharseVipCommand>(purcharseVipCommandMemory);

        //var isCheckSubscription = await unitOfWork.SubscriptionRepository
        //    .FindSingleAsync(s => s.UserId == purcharseVipCommand.UserId);

        //// If Subscription found by UserId have IsActive = true(subscription not expired date) => Exception
        //if (isCheckSubscription != null)
        //{
        //    throw new UserException.SubscriptionActivedException();
        //}

        //var subscriptionPackage = await unitOfWork.SubscriptionPackageRepository.
        //    FindSingleAsync(sb => sb.Id == purcharseVipCommand.SubscriptionPackageId);

        //var subscription = Domain.Models.Subscription.Create(Guid.NewGuid(), subscriptionPackage, purcharseVipCommand.UserId);

        //var donate = Domain.Models.Donate.CreateDonate(purcharseVipCommand.)
        //unitOfWork.SubscriptionRepository.Add(subscription);

        //await unitOfWork.SaveChangesAsync(cancellationToken);

        //await responseCacheService.DeleteCacheResponseAsync($"subscribe_{command.OrderId}");
        //var result = ClientSetting.Value.Url + "/" + ClientSetting.Value.PurcharseSuccess + "/" + subscription.Id;
        //return Result.Success(result);
        throw new Exception("");
    }
}