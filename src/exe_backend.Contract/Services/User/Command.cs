namespace exe_backend.Contract.Services.User;

public static class Command
{
    public record PurcharseVipCommand(Guid UserId, Guid SubscriptionPackageId) : ICommand;
    public record CancelVipCommand(Guid UserId) : ICommand;
    public record RenewVipCommand(Guid UserId) : ICommand;

}