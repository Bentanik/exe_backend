namespace exe_backend.Contract.Services.User;

public static class Command
{
    public record PurcharseVipCommand(Guid UserId, Guid SubscriptionPackageId)
    : ICommand;
    public record SuccessPurcharseVipCommand(long OrderId) : ICommand;
    public record FailPurcharseVipCommand(long OrderId) : ICommand<Response.FailPurcharseVipResponse>;
    public record CancelVipCommand(Guid UserId) : ICommand;
    public record RenewVipCommand(Guid UserId) : ICommand;
}