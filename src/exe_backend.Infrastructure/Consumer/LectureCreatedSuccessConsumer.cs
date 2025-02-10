using exe_backend.Contract.DTOs.MediaDTOs;
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
        var videoFilePath = context.Message.VideoFilePath;
        try
        {
            var imageUploadTask = UploadImageAsync(lectureCreatedSuccessEvent.LectureDTO.Name!, imageFilePath);
            var videoUploadTask = UploadVideoAsync(lectureCreatedSuccessEvent.LectureDTO.Name!, videoFilePath);

            await Task.WhenAll(imageUploadTask, videoUploadTask);

            var imageDto = await imageUploadTask;
            var videoDto = await videoUploadTask;
            // LectureDTO
            var lectureDto = lectureCreatedSuccessEvent.LectureDTO with
            {
                Image = imageDto,
                Video = videoDto
            };

            await sender.Send(new Command.SaveImageAndVideoLectureCommand(lectureDto));
        }
        catch (Exception ex)
        {
            System.Console.WriteLine(ex.Message.ToString());
        }
        finally
        {
            File.Delete(imageFilePath);
            File.Delete(videoFilePath);
            Console.WriteLine($"File {imageFilePath} deleted.");
            System.Console.WriteLine($"File {videoFilePath} deleted");
        }
    }

    private async Task<ImageDTO> UploadImageAsync(string name, string filePath)
    {
        using var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
        var uploadedImage = await mediaService.UploadImageAsync(name, fileStream);
        return uploadedImage;
    }

    private async Task<VideoDTO> UploadVideoAsync(string name, string filePath)
    {
        using var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
        var uploadedVideo = await mediaService.UploadVideoAsync(name, fileStream);
        return uploadedVideo;
    }
}