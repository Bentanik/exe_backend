namespace exe_backend.Domain.ValueObjects;

public record Video
{
   public Video(string id, double duration)
    {
        Id = id;
        Duration = duration;
    }

    public string Id { get; } // VideoId
    public double Duration { get; } // Duration a video
    public static Video Of(string id, double duration)
    {
        return new Video(id, duration);
    }
}