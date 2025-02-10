using exe_backend.Contract.DTOs.CourseDTOs;
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
        var fileName = $"course_{courseDto.Name}";
        var tempFilePath = Path.Combine(Path.GetTempPath(), fileName);

        // Save file in temp
        using (var fileStream = new FileStream(tempFilePath, FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite))
        {
            await thumbnailFile.CopyToAsync(fileStream);
        }
        var courseCreatedSuccessEvent = new CourseCreatedSuccessEvent
        {
            CourseDTO = courseDto,
            FilePath = tempFilePath,
        };

        await publishEndpoint.Publish(courseCreatedSuccessEvent);
    }
}
