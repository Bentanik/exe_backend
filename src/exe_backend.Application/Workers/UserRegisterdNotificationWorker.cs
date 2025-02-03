using exe_backend.Contract.DTOs.UserDTOs;

namespace exe_backend.Application.Workers;

public class UserRegisteredNotificationWorker
    (IEmailService emailService)
    : IJob
{
    public async Task Execute(IJobExecutionContext context)
    {
        var jobDataMap = context.JobDetail.JobDataMap;

        var userDtoJson = jobDataMap.GetString(nameof(UserDto));

        var userDto = JsonConvert.DeserializeObject<UserDto>(userDtoJson!);

        // Send email notification
        await emailService.SendMailAsync(userDto!.Email!, "Đăng ký thành công", "UserCreatedEmail.html",
        new Dictionary<string, string> {
              {"ToEmail", userDto.Email!},
              {"UserId", userDto.Id.ToString()! },
        });
        await Task.CompletedTask;
    }
}
