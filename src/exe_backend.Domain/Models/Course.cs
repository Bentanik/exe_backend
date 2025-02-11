using System.Text.Json.Serialization;
using exe_backend.Domain.ValueObjects;

namespace exe_backend.Domain.Models;

public class Course : DomainEntity<Guid>
{
    public string Name { get; private set; } = default!;
    public string Description { get; private set; } = default!;
    public Image? Thumbnail { get; private set; }
    
    [JsonIgnore]
    public ICollection<Chapter> Chapters { get; set; } = [];
    public int QuantityChapters
    {
        get => Chapters.Count;
        private set { }
    }

    public static Course Create(Guid id, string name, string description, Image? thumbnail = null)
    {
        var course = new Course
        {
            Id = id,
            Name = name,
            Description = description,
            Thumbnail = thumbnail,
            IsDeleted = false
        };

        return course;
    }

    public void Update(Image? thumbnail = null)
    {
        if (thumbnail != null) Thumbnail = thumbnail;
    }
}