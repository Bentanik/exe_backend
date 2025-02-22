using System.Text.Json.Serialization;
using exe_backend.Contract.Exceptions.BussinessExceptions;

namespace exe_backend.Domain.Models;

public class Chapter : DomainEntity<Guid>
{
    public string Name { get; private set; } = default!;
    public string Description { get; private set; } = default!;
    public Guid? CourseId { get; set; }

    [JsonIgnore]
    public Course? Course { get; set; } = default!;

    [JsonIgnore]
    public ICollection<Lecture> Lectures { get; set; } = [];
    public int QuantityLectures
    {
        get => Lectures.Count;
        private set { }
    }
    public double TotalDurationLectures
    {
        get => Lectures?.Sum(lt => lt.VideoLecture?.Duration ?? 0) ?? 0;
        private set { }
    }
    public static Chapter Create(Guid id, string name, string description)
    {
        var chapter = new Chapter
        {
            Id = id,
            Name = name,
            Description = description,
            IsDeleted = false
        };

        return chapter;
    }

    public void AssignToCourse(Course course)
    {
        if (course == null)
            throw new CourseException.CourseNotFoundException();

        CourseId = course.Id;
    }

    public void AssignToLecture(ICollection<Lecture> lectures)
    {
        Lectures = lectures;
    }
}