using exe_backend.Contract.DTOs.CourseDTOs;
using exe_backend.Contract.Helpers;
using Microsoft.AspNetCore.Http;

namespace exe_backend.Application.UseCases.V1.Events;

public sealed class CreateCourseEventHandler
    (IPublishEndpoint publishEndpoint)
    : IDomainEventHandler<Contract.Services.Course.Event.CreatedCourseEvent>
{
    public async Task Handle(Contract.Services.Course.Event.CreatedCourseEvent notification, CancellationToken cancellationToken)
    {
        await SaveThumbnailAsync(notification.CourseDto, notification.ThumbnailFile);
    }

    private async Task SaveThumbnailAsync(CourseDTO courseDto, IFormFile thumbnailFile)
    {
        var imageBytes = await FileHelper.ConvertToByteArrayAsync(thumbnailFile);

        var courseCreatedSuccessEvent = new CourseCreatedSuccessEvent
        {
            CourseDTO = courseDto,
            ThumbnailFilePath = imageBytes,
        };

        await publishEndpoint.Publish(courseCreatedSuccessEvent);
    }
}
