using exe_backend.Contract.DTOs.PaymentDTOs;
using Net.payOS;
using Net.payOS.Types;

namespace exe_backend.Infrastructure.Services;

public class PaymentService : IPaymentService
{
    private readonly PayOS _payOS;
    public PaymentService(IOptions<PayOSSetting> payOSConfig)
    {
        _payOS = new PayOS(payOSConfig.Value.ClientId, payOSConfig.Value.ApiKey, payOSConfig.Value.ChecksumKey);
    }

    public async Task<CreatePaymentResponseDTO> CreatePaymentLink(CreatePaymentDTO paymentData)
    {
        try
        {
            int totalAmount = paymentData.Items.Sum(item => item.Quantity * item.Price);

            List<ItemData> items = [.. paymentData.Items.Select(item =>
                new ItemData(item.ItemName, item.Quantity, item.Price))];

            var data = new PaymentData(paymentData.OrderCode, totalAmount, paymentData.Description, items, paymentData.CancelUrl, paymentData.ReturnUrl);

            CreatePaymentResult createPaymentResult = await _payOS.createPaymentLink(data);

            var responseDTO = new CreatePaymentResponseDTO(true, createPaymentResult.checkoutUrl, "Success");

            return responseDTO;
        }
        catch (System.Exception exception)
        {
            return null;
        }

    }

    public async Task<bool> CancelOrder(long orderId)
    {
        try
        {
            PaymentLinkInformation paymentLinkInformation = await _payOS.cancelPaymentLink(orderId);
            return true;
        }
        catch (System.Exception exception)
        {
            return false;
        }
    }

}