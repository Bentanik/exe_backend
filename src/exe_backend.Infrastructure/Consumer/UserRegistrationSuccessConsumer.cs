namespace exe_backend.Infrastructure.Consumer;

public class UserRegistrationSuccessConsumer
    (IEmailService emailService,
    ILogger<UserRegistrationSuccessConsumer> logger)
    : IConsumer<UserRegistrationSuccessEvent>
{
    public async Task Consume(ConsumeContext<UserRegistrationSuccessEvent> context)
    {
        logger.LogInformation("Integration Event handled: {IntegrationEvent}", context.Message.GetType().Name);

        // Send to type notification
        var userRegistrationSuccessEvent = context.Message;

        if (userRegistrationSuccessEvent == null) await Task.CompletedTask;

        await emailService.SendMailAsync(userRegistrationSuccessEvent!.User_Email, "Đăng ký thành công", "UserCreatedEmail.html",
        new Dictionary<string, string> {
              {"ToEmail", userRegistrationSuccessEvent.User_Email!},
              {"UserId", userRegistrationSuccessEvent.User_Id.ToString()},
        });
    }
}
