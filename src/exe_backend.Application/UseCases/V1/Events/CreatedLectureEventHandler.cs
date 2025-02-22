using exe_backend.Contract.DTOs.CourseDTOs;
using Microsoft.AspNetCore.Http;
using static exe_backend.Contract.Services.Course.Event;

namespace exe_backend.Application.UseCases.V1.Events;

public sealed class CreatedLectureEventHandler
    (IPublishEndpoint publishEndpoint)
    : IDomainEventHandler<CreatedLectureEvent>
{
    public async Task Handle(CreatedLectureEvent notification, CancellationToken cancellationToken)
    {
        await SaveImageAsync(notification.LectureDto, notification.ImageFile);
    }

    private async Task SaveImageAsync(LectureDTO lectureDto, IFormFile lectureImageFile)
    {
        var imageBytes = await FileHelper.ConvertToByteArrayAsync(lectureImageFile);

          var lectureCreatedSuccessEvent = new LectureCreatedSuccessEvent
        {
            LectureDTO = lectureDto,
            ImageFilePath = imageBytes
        };

        await publishEndpoint.Publish(lectureCreatedSuccessEvent);
    }
}
