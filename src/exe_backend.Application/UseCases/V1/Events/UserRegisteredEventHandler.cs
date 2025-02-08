using exe_backend.Contract.DTOs.UserDTOs;

namespace exe_backend.Application.UseCases.V1.Events;

public sealed class UserRegisteredEventHandler
    (IPublishEndpoint publishEndpoint)
    : IDomainEventHandler<Contract.Services.Auth.Event.UserRegisterdEvent>
{
    public async Task Handle(Contract.Services.Auth.Event.UserRegisterdEvent notification, CancellationToken cancellationToken)
    {
        await SendNotificationAsync(notification.UserDto);
    }

    private async Task SendNotificationAsync(UserDto userDto)
    {
        var newEvent = new UserRegistrationSuccessEvent
        {
            User_Id = (Guid)userDto.Id!,
            User_Email = userDto.Email!,
            User_FullName = userDto.FullName!,
        };

        await publishEndpoint.Publish(newEvent);
    }
}
