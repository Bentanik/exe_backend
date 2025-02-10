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
        await SaveImageAndVideoAsync(notification.LectureDto, notification.ImageFile, notification.VideoFile);
    }

    private async Task SaveImageAndVideoAsync(LectureDTO lectureDto, IFormFile lectureImageFile, IFormFile lectureVideoFile)
    {
        // Generate unique filenames for the image and video
        var imageFileName = $"lecture_{lectureDto.Id}_image{Path.GetExtension(lectureImageFile.FileName)}";
        var videoFileName = $"lecture_{lectureDto.Id}_video{Path.GetExtension(lectureVideoFile.FileName)}";

        var tempImagePath = Path.Combine(Path.GetTempPath(), imageFileName);
        var tempVideoPath = Path.Combine(Path.GetTempPath(), videoFileName);

        // Save image file in temp directory
        using (var imageFileStream = new FileStream(tempImagePath, FileMode.Create, FileAccess.Write, FileShare.ReadWrite))
        {
            await lectureImageFile.CopyToAsync(imageFileStream);
        }

        // Save video file in temp directory
        using (var videoFileStream = new FileStream(tempVideoPath, FileMode.Create, FileAccess.Write, FileShare.ReadWrite))
        {
            await lectureVideoFile.CopyToAsync(videoFileStream);
        }

        var lectureCreatedSuccessEvent = new LectureCreatedSuccessEvent
        {
            LectureDTO = lectureDto,
            ImageFilePath = tempImagePath,
            VideoFilePath = tempVideoPath
        };

        await publishEndpoint.Publish(lectureCreatedSuccessEvent);
    }
}
