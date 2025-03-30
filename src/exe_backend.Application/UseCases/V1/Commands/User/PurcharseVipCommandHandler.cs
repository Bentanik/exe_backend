using exe_backend.Contract.DTOs.PaymentDTOs;
using exe_backend.Contract.Services.User;

namespace exe_backend.Application.UseCases.V1.Commands.User;

public sealed class PurcharseVipCommandHandler : ICommandHandler<Command.PurcharseVipCommand>
{
    private readonly IPaymentService _paymentService;
    private readonly PayOSSetting _payOSSetting;
    private readonly IResponseCacheService _responseCacheService;
    private readonly IUnitOfWork _unitOfWork;

    public PurcharseVipCommandHandler(
        IPaymentService paymentService,
       IOptions<PayOSSetting> payOSSetting
,
       IResponseCacheService responseCacheService,
       IUnitOfWork UnitOfWork)
    {
        _paymentService = paymentService;
        _payOSSetting = payOSSetting.Value;
        _responseCacheService = responseCacheService;
        _unitOfWork = UnitOfWork;
    }

    public async Task<Result> Handle(Command.PurcharseVipCommand command, CancellationToken cancellationToken)
    {
        //var isCheckSubscription = await _unitOfWork.SubscriptionRepository
        //    .FindSingleAsync(s => s.UserId == command.UserId);

        //// If Subscription found by UserId have IsActive = true(subscription not expired date) => Exception
        //if (isCheckSubscription != null)
        //{
        //    throw new UserException.SubscriptionActivedException();
        //}

        //var subscriptionPackage = await _unitOfWork.SubscriptionPackageRepository.FindSingleAsync(sb => sb.Id == command.SubscriptionPackageId);

        //if (subscriptionPackage == null)
        //{
        //    throw new SubscriptionException.SubscriptionNotFoundException();
        //}

        //long orderId = new Random().Next(1, 100000);
        //// Create payment dto
        //List<ItemDTO> itemDTOs = [new ItemDTO(subscriptionPackage.Name, 1, subscriptionPackage.Price)];

        //var createPaymentDto = new CreatePaymentDTO(orderId, "Ủng hộ chúng tôi", itemDTOs, _payOSSetting.ErrorUrl + $"?orderId={orderId}", _payOSSetting.SuccessUrl + $"?orderId={orderId}");

        //var result = await _paymentService.CreatePaymentLink(createPaymentDto);

        //// Save memory to when success or fail will know value
        //await _responseCacheService.SetCacheResponseAsync($"subscribe_{orderId}", command, TimeSpan.FromMinutes(60));

        //return Result.Success(new Success<CreatePaymentResponseDTO>("", "", result));
        throw new Exception("");
    }
}
