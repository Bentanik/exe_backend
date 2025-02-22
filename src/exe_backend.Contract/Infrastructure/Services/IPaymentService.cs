using exe_backend.Contract.DTOs.PaymentDTOs;

namespace exe_backend.Contract.Infrastructure.Services;
public interface IPaymentService
{
    Task<CreatePaymentResponseDTO> CreatePaymentLink(CreatePaymentDTO paymentData);
    Task<bool> CancelOrder(long orderId);
}
