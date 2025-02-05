using exe_backend.Contract.DTOs.UserDTOs;
using exe_backend.Contract.Services.Auth;

namespace exe_backend.Application.UseCases.V1.Events;

public sealed class UserPasswordResetConfirmedEvent
    (ISchedulerFactory schedulerFactory)
    : IDomainEventHandler<Event.UserPasswordResetConfirmedEvent>
{
    public async Task Handle(Event.UserPasswordResetConfirmedEvent notification, CancellationToken cancellationToken)
    {
        await SendNotificationForgotPasswordAsync(notification.UserDto, notification.Token);
    }

    private async Task SendNotificationForgotPasswordAsync(UserDto userDto, string forgotPasswordToken)
    {
        // Schedule to do the job
        var scheduler = await schedulerFactory.GetScheduler();

        if (!scheduler.IsStarted)
        {
            await scheduler.Start();
        }

        var job = JobBuilder.Create<UserPasswordResetConfirmedNotificationWorker>()
            .WithIdentity($"UserResetPasswordConfirmedNotification_{userDto.Id}", "UserResetPasswordConfirmedNotification")
            .UsingJobData(nameof(UserDto), JsonConvert.SerializeObject(userDto))
            .UsingJobData("forgotPasswordToken", forgotPasswordToken)
            .Build();

        var trigger = TriggerBuilder.Create()
            .WithIdentity($"Trigger_UserResetPasswordConfirmedNotification_{userDto.Id}", "UserResetPasswordConfirmedNotification")
            .StartNow()
            .Build();

        await scheduler.ScheduleJob(job, trigger);
    }
}
