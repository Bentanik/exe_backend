namespace exe_backend.Contract.Services.Course;

public static class Command
{
    public record CreateCourseCommand(string Email, string Password, string FullName) : ICommand;
}