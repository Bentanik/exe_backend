namespace exe_backend.Contract.Services.Course;

public static class Command
{
    public record CreateCourseCommand(string Name, string Description, IFormFile ThumbnailFile) : ICommand;
}