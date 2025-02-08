namespace exe_backend.Contract.IntegrationEvents;
public record UserConfirmedPasswordSuccessEvent : IntegrationEvent
{
    public string User_Email { get; set; } = default!;
    public string Password_TokenVerify { get; set; } = default!;
}