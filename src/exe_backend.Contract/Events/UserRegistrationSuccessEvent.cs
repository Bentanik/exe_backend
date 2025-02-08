namespace exe_backend.Contract.Events;

public record UserRegistrationSuccessEvent : IntegrationEvent
{
    public Guid User_Id { get; set; }
    public string User_FullName { get; set; } = default!;
    public string User_Email {get;set;} = default!;

}