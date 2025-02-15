namespace exe_backend.Contract.DTOs.PaymentDTOs;

public sealed class CreatePaymentRequestDTO
{
    public long OrderId { get; set; }
    public int Amount { get; set; }
    public string Description { get; set; }
}
