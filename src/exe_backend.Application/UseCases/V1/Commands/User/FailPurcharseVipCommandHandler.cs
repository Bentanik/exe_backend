using exe_backend.Contract.Services.User;

namespace exe_backend.Application.UseCases.V1.Commands.User;

public sealed class FailDonateBankingQueryHandler
    : ICommandHandler<Command.FailPurcharseVipCommand, Contract.Services.User.Response.FailPurcharseVipResponse>
{
    private readonly IResponseCacheService _responseCacheService;
    private readonly ClientSetting _clientSetting;
    private readonly IPaymentService _paymentService;

    public FailDonateBankingQueryHandler(IResponseCacheService responseCacheService,
        IOptions<ClientSetting> clientConfig,
        IPaymentService paymentService)
    {
        _responseCacheService = responseCacheService;
        _clientSetting = clientConfig.Value;
        _paymentService = paymentService;
    }

    public async Task<Result<Contract.Services.User.Response.FailPurcharseVipResponse>> Handle(Command.FailPurcharseVipCommand command, CancellationToken cancellationToken)
    {
        await _paymentService.CancelOrder((command.OrderId));
        await _responseCacheService.DeleteCacheResponseAsync($"subscribe_{command.OrderId}");

        return Result.Success(new Contract.Services.User.Response.FailPurcharseVipResponse($"{_clientSetting.Url}{_clientSetting.PurcharseFail}"));
    }
}
