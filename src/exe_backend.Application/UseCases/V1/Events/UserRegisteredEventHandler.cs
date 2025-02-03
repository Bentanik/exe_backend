using exe_backend.Contract.DTOs.UserDTOs;
using exe_backend.Contract.Services.Auth;

namespace exe_backend.Application.UseCases.V1.Events;

public sealed class UserRegisteredEventHandler
    (ISchedulerFactory schedulerFactory)
    : IDomainEventHandler<Event.UserRegisterdEvent>
{
    public async Task Handle(Event.UserRegisterdEvent notification, CancellationToken cancellationToken)
    {
        await SendNotificationAsync(notification.UserDto);
    }

    private async Task SendNotificationAsync(UserDto userDto)
    {
        // Schedule to do the job
        var scheduler = await schedulerFactory.GetScheduler();

        if (!scheduler.IsStarted)
        {
            await scheduler.Start();
        }

        var job = JobBuilder.Create<UserRegisteredNotificationWorker>()
            .WithIdentity($"UserRegisteredNotification_{userDto.Id}", "UserRegisteredNotification")
            .UsingJobData(nameof(UserDto), JsonConvert.SerializeObject(userDto))
            .Build();

        var trigger = TriggerBuilder.Create()
            .WithIdentity($"Trigger_UserRegisteredNotification_{userDto.Id}", "UserRegisteredNotificationWorker")
            .StartNow()
            .Build();

        await scheduler.ScheduleJob(job, trigger);
    }
}
