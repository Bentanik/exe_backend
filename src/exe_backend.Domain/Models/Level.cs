using System.Text.Json.Serialization;

namespace exe_backend.Domain.Models;

public class Level : DomainEntity<Guid>
{
    public string Name { get; private set; } = default!;

    [JsonIgnore]
    public ICollection<Course> Courses { get; set; } = [];

    public int QuantityCourses
    {
        get => Courses.Count;
        private set { }
    }

    public static Level Create(Guid id, string name)
    {
        var level = new Level
        {
            Id = id,
            Name = name,
            IsDeleted = false
        };

        return level;
    }
}