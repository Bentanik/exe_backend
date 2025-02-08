namespace exe_backend.Infrastructure.Consumer;

public class UserConfirmedPasswordSuccessConsumer
    (IEmailService emailService,
    ILogger<UserConfirmedPasswordSuccessConsumer> logger)
    : IConsumer<UserConfirmedPasswordSuccessEvent>
{
    public async Task Consume(ConsumeContext<UserConfirmedPasswordSuccessEvent> context)
    {
        logger.LogInformation("Integration Event handled: {IntegrationEvent}", context.Message.GetType().Name);

        // Send to type notification
        var userConfirmedPasswordSuccessEvent = context.Message;

        if (userConfirmedPasswordSuccessEvent == null) await Task.CompletedTask;

        await emailService.SendMailAsync(userConfirmedPasswordSuccessEvent!.User_Email, "Xác nhận thay đổi mật khẩu", "UserCreatedEmail.html",
        new Dictionary<string, string> {
              {"ToEmail", userConfirmedPasswordSuccessEvent.User_Email!},
              {"UserId", userConfirmedPasswordSuccessEvent.Password_TokenVerify},
        });
    }
}
