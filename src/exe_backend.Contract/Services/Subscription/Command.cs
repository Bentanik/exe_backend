namespace exe_backend.Contract.Services.Subscription;

public static class Command
{
    public record CreateSubscriptionPackageCommand(string Name, int Price, int ExpiredMonth, string? Description = null) : ICommand;
}