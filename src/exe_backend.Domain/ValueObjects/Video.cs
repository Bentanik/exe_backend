namespace exe_backend.Domain.ValueObjects;

public record Video
{
    // PublicId of Youtube
   public Video(string publicId, double duration)
    {
        PublicId = publicId;
        Duration = duration;
    }

    public string PublicId { get; } // VideoId
    public double Duration { get; } // Duration a video
    public static Video Of(string publicId, double duration)
    {
        return new Video(publicId, duration);
    }
}