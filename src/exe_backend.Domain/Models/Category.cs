using System.Text.Json.Serialization;

namespace exe_backend.Domain.Models;

public class Category : DomainEntity<Guid>
{
    public string Name { get; private set; } = default!;

    [JsonIgnore]
    public ICollection<Course> Courses { get; set; } = [];

    public int QuantityCourses
    {
        get => Courses.Count;
        private set { }
    }

    public static Category Create(Guid id, string name)
    {
        var category = new Category
        {
            Id = id,
            Name = name,
            IsDeleted = false
        };

        return category;
    }
}