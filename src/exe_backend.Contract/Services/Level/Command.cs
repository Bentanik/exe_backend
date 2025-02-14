namespace exe_backend.Contract.Services.Level;

public static class Command
{
    public record CreateLevelCommand(string Name) : ICommand;
}