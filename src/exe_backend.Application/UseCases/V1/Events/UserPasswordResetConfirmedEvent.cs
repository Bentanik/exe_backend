using exe_backend.Contract.DTOs.UserDTOs;

namespace exe_backend.Application.UseCases.V1.Events;

public sealed class UserPasswordResetConfirmedEvent
    (IPublishEndpoint publishEndpoint)
    : IDomainEventHandler<Contract.Services.Auth.Event.UserPasswordResetConfirmedEvent>
{
    public async Task Handle(Contract.Services.Auth.Event.UserPasswordResetConfirmedEvent notification, CancellationToken cancellationToken)
    {
        await SendNotificationForgotPasswordAsync(notification.UserDto, notification.Token);
    }

    private async Task SendNotificationForgotPasswordAsync(UserDto userDto, string forgotPasswordToken)
    {
        var newEvent = new UserConfirmedPasswordSuccessEvent
        {
            User_Email = userDto.Email!,
            Password_TokenVerify = forgotPasswordToken
        };

        await publishEndpoint.Publish(newEvent);
    }
}
