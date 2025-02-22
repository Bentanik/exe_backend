namespace exe_backend.Contract.Services.Category;

public static class Command
{
    public record CreateCategoryCommand(string Name) : ICommand;
}