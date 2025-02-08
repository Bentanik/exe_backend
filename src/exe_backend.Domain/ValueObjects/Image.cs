namespace exe_backend.Domain.ValueObjects;

public record Image
{
    public Image(string publicId, string publicUrl)
    {
        PublicId = publicId;
        PublicUrl = publicUrl;
    }

    public string PublicId { get; } // Image Id
    public string PublicUrl { get; } // Image Url

    public static Image Of(string publicId, string publicUrl)
    {
        return new Image(publicId, publicUrl);
    }
}