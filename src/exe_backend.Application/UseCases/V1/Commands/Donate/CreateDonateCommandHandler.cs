using exe_backend.Contract.DTOs.PaymentDTOs;
using exe_backend.Contract.Services.Donate;
using static exe_backend.Contract.Services.Donate.Command;

namespace exe_backend.Application.UseCases.V1.Commands.Donate;

public sealed class CreateDonateCommandHandler
    : ICommandHandler<Command.CreateDonateCommand>
{
    private readonly IPaymentService _paymentService;
    private readonly PayOSSetting _payOSSetting;
    private readonly IResponseCacheService _responseCacheService;
    private readonly IUnitOfWork _unitOfWork;

    public CreateDonateCommandHandler(
       IPaymentService paymentService,
       IOptions<PayOSSetting> payOSSetting,
       IResponseCacheService responseCacheService,
       IUnitOfWork UnitOfWork)
    {
        _paymentService = paymentService;
        _payOSSetting = payOSSetting.Value;
        _responseCacheService = responseCacheService;
        _unitOfWork = UnitOfWork;
    }
    public async Task<Result> Handle(Command.CreateDonateCommand request, CancellationToken cancellationToken)
    {
        long orderId = new Random().Next(1, 100000);
        // Create payment dto
        List<ItemDTO> itemDTOs = [new ItemDTO("Ủng hộ chúng tôi", 1, request.Price)];

        var createPaymentDto = new CreatePaymentDTO(orderId, "Ủng hộ chúng tôi", itemDTOs, _payOSSetting.ErrorUrl + $"?orderId={orderId}", _payOSSetting.SuccessUrl + $"?orderId={orderId}");

        var result = await _paymentService.CreatePaymentLink(createPaymentDto);

        // Save memory to when success or fail will know value
        await _responseCacheService.SetCacheResponseAsync($"subscribe_{orderId}", request, TimeSpan.FromMinutes(180));

        return Result.Success(new Success<CreatePaymentResponseDTO>("", "", result));
    }
}
