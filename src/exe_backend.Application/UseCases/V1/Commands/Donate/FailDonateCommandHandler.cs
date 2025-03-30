using exe_backend.Contract.Services.Donate;

namespace exe_backend.Application.UseCases.V1.Commands.Donate;

public sealed class FailDonateCommandHandler
    : ICommandHandler<Command.FailDonateCommand, string>
{
    private readonly IResponseCacheService _responseCacheService;
    private readonly ClientSetting _clientSetting;
    private readonly IPaymentService _paymentService;

    public FailDonateCommandHandler(IResponseCacheService responseCacheService,
        IOptions<ClientSetting> clientConfig,
        IPaymentService paymentService)
    {
        _responseCacheService = responseCacheService;
        _clientSetting = clientConfig.Value;
        _paymentService = paymentService;
    }
    public async Task<Result<string>> Handle(Command.FailDonateCommand command, CancellationToken cancellationToken)
    {
        await _paymentService.CancelOrder((command.OrderId));
        await _responseCacheService.DeleteCacheResponseAsync($"subscribe_{command.OrderId}");

        var result = _clientSetting.Url + "/" + _clientSetting.PurcharseFail;
        return Result.Success(result);
    }
}
