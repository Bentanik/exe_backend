namespace exe_backend.Contract.Infrastructure.Services;

public interface IMediaService
{
    Task<ImageDTO> UploadImageAsync(string fileName, Stream fileStream);
    Task<List<ImageDTO>> UploadImagesAsync(List<IFormFile> fileImages);
    Task<bool> DeleteFileAsync(string publicId);
    Task<VideoDTO?> UploadVideoAsync(string fileName, Stream fileStream);
    Task<bool> Test();
}