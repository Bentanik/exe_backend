using exe_backend.Contract.Helpers;
using exe_backend.Contract.Services.Course;

namespace exe_backend.Infrastructure.Consumer;

public class CourseCreatedSuccessConsumer
    (ISender sender, IMediaService mediaService,
    ILogger<CourseCreatedSuccessConsumer> logger)
    : IConsumer<CourseCreatedSuccessEvent>
{

    public async Task Consume(ConsumeContext<CourseCreatedSuccessEvent> context)
    {
        logger.LogInformation("Integration Event handled: {IntegrationEvent}", context.Message.GetType().Name);

        var courseCreatedSuccessEvent = context.Message;
        var thumbnailFilePath = context.Message.ThumbnailFilePath;
        try
        {
            var thumbnailFileStream = FileHelper.MergeToMemoryStream(thumbnailFilePath);

            // Upload file
            var mediaUpploaded = await mediaService.UploadImageAsync(courseCreatedSuccessEvent.CourseDTO.Name!, thumbnailFileStream);

            // CourseDTO
            var courseDto = courseCreatedSuccessEvent.CourseDTO with
            {
                Thumbnail = mediaUpploaded
            };

            // Send to save thumbnail course command
            await sender.Send(new Command.SaveThumbnailCourseCommand(courseDto));

            Console.WriteLine($"Complete upload file course {courseDto.Name}");
        }
        catch (Exception ex)
        {
            System.Console.WriteLine(ex.Message.ToString());
        }
    }
}
