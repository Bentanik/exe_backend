namespace exe_backend.Contract.Services.Donate;

public static class Command
{
    public record CreateDonateCommand(int Price, string? Description = null, Guid? UserId = null) : ICommand;
    public record SuccessDonateCommand(long OrderId) : ICommand<string>;
    public record FailDonateCommand(long OrderId) : ICommand<string>;
}