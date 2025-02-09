using exe_backend.Contract.DTOs.MediaDTOs;
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
        var filePath = context.Message.FilePath;
        try
        {
            // Get file from filePath, ex filePath: temp
            using var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);

            // Upload file
            var mediaUpploaded = await mediaService.UploadImageAsync(courseCreatedSuccessEvent.CourseDTO.Name!, fileStream);

            // CourseDTO
            var courseDto = courseCreatedSuccessEvent.CourseDTO with
            {
                Thumbnail = mediaUpploaded
            };

            // Send to save thumbnail course command
            await sender.Send(new Command.SaveThumbnailCourseCommand(courseDto));

        }
        catch (Exception ex)
        {
            System.Console.WriteLine(ex.Message.ToString());
        }
        finally
        {
            File.Delete(filePath);
            Console.WriteLine($"File {filePath} deleted.");
        }
    }
}
