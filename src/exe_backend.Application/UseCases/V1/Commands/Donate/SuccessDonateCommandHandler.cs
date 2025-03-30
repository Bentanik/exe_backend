using exe_backend.Contract.Services.Donate;

namespace exe_backend.Application.UseCases.V1.Commands.Donate;

public sealed class SuccessDonateCommandHandler
     (IUnitOfWork unitOfWork, IResponseCacheService responseCacheService, IOptions<ClientSetting> clientSetting)
    : ICommandHandler<Command.SuccessDonateCommand, string>
{
    public async Task<Result<string>> Handle(Command.SuccessDonateCommand command, CancellationToken cancellationToken)
    {
        //Get infomation saved in memory
        var donateCommandMemory = await responseCacheService.GetCacheResponseAsync($"subscribe_{command.OrderId}");

        // Conver JSON to object
        var donateCommand = JsonConvert.DeserializeObject<Command.CreateDonateCommand>(donateCommandMemory);

        var donate = Domain.Models.Donate.CreateDonate(donateCommand.Price, donateCommand.Description, command.OrderId);

        if(donateCommand.UserId != null && donateCommand.UserId != Guid.Empty)
        {
            donate.AssignUser((Guid)donateCommand.UserId);
        }

        unitOfWork.DonateRepository.Add(donate);
        await unitOfWork.SaveChangesAsync();

        var result = clientSetting.Value.Url + "/" + clientSetting.Value.PurcharseSuccess + "/" + donate.Id;
        return Result.Success(result);
    }
}
