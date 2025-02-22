using exe_backend.Contract.Helpers;
using exe_backend.Contract.Services.Course;

namespace exe_backend.Infrastructure.Consumer;

public class LectureCreatedSuccessConsumer
    (ISender sender, IMediaService mediaService,
    ILogger<LectureCreatedSuccessConsumer> logger)
    : IConsumer<LectureCreatedSuccessEvent>
{
    public async Task Consume(ConsumeContext<LectureCreatedSuccessEvent> context)
    {
        logger.LogInformation("Integration Event handled: {IntegrationEvent}", context.Message.GetType().Name);

        var lectureCreatedSuccessEvent = context.Message;
        var imageFilePath = context.Message.ImageFilePath;
        try
        {
            var imageFileStream = FileHelper.MergeToMemoryStream(imageFilePath);

            // Upload file
            var mediaUpploaded = await mediaService.UploadImageAsync(lectureCreatedSuccessEvent.LectureDTO.Name!, imageFileStream);

            // CourseDTO
            var lectureDto = lectureCreatedSuccessEvent.LectureDTO with
            {
                ImageLecture = mediaUpploaded,
            };

            await sender.Send(new Command.SaveImageLectureCommand(lectureDto));

            Console.WriteLine($"Complete upload file course {lectureDto.Name}");
        }
        catch (Exception ex)
        {
            System.Console.WriteLine(ex.Message.ToString());
        }
    }
}