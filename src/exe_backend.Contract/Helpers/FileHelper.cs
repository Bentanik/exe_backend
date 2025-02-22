namespace exe_backend.Contract.Helpers;

public static class FileHelper
{
    public static async Task<byte[]> ConvertToByteArrayAsync(IFormFile file)
    {
        if (file == null || file.Length == 0)
            return null;

        using (var memoryStream = new MemoryStream())
        {
            await file.CopyToAsync(memoryStream);
            return memoryStream.ToArray();
        }
    }

    public static MemoryStream MergeToMemoryStream(byte[] fileBytes)
    {
        if (fileBytes == null || fileBytes.Length == 0)
            return null;
        
        return new MemoryStream(fileBytes);
    }
}