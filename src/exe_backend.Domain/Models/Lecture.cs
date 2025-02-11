using System.Text.Json.Serialization;
using exe_backend.Contract.Exceptions.BussinessExceptions;
using exe_backend.Domain.ValueObjects;

namespace exe_backend.Domain.Models;

public class Lecture : DomainEntity<Guid>
{
    public string Name { get; private set; } = default!;
    public string Description { get; private set; } = default!;
    public Image? ImageLecture { get; private set; } = default!;
    public Video? VideoLecture { get; private set; } = default!;
    public Guid? ChapterId { get; private set; }
    
    [JsonIgnore]
    public Chapter? Chapter { get; set; } = default!;
    
    public static Lecture Create(Guid id, string name, string description, Image? imageLecture = null, Video? videoLecture = null)
    {
        var lecture = new Lecture
        {
            Id = id,
            Name = name,
            Description = description,
            ImageLecture = imageLecture,
            VideoLecture = videoLecture,
            IsDeleted = false
        };

        return lecture;
    }

    public void Update(Image? imageLecture = null, Video? videoLecture = null)
    {
        if (imageLecture != null) ImageLecture = imageLecture;
        if (videoLecture != null) VideoLecture = videoLecture;
    }

    public void AssignToChapter(Chapter chapter)
    {
        if (chapter == null)
            throw new CourseException.ChapterNotFoundException();

        ChapterId = chapter.Id;
    }
}